using Microsoft.Extensions.Logging;
using Polly;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XupMonitor.DataAccess.Entities;
using XupMonitor.DataAccess.Repositories;
using XupMonitor.Helper;
using XupMonitor.Helper.Enum;

namespace XupMonitor
{
    public class MonitorJob : IJob
    {
        private readonly ILogger<MonitorJob> _logger;
        private readonly JobSchedule _jobSchedules;      
        readonly ConnectionStrings _config;
        readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        public MonitorJob(ILogger<MonitorJob> logger, JobSchedule jobSchedules,
                       ConnectionStrings config,AppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _jobSchedules = jobSchedules;
            _config = config;
            _appSettings = appSettings;
            _httpClientFactory = httpClientFactory;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("------------------------------------------------------");
            var schedule = context.Scheduler;           
           
            //Default job runs
            using (var uow = new UnitofWork(_config))
            {                
               var result =  uow.CheckRegisterRepository.GetCheckUpdates().ToList();
             
                if (result.Any() && context.JobDetail.Key.Name!= "MonitorMainJob")
                {
                    //match the curent job to be executed
                    var currentData = result.Where(x => x.Id == new Guid(context.JobDetail.Key.Name)
                                                                  && x.IsActive && x.IsScheduled).FirstOrDefault();
                    if (currentData != null)
                    {
                        _logger.LogInformation($"url execution Begin");
                        // check url
                        Task taskexecute =Task.Run(()=> RunUrl(currentData)
                                                 .ContinueWith((data)=> 
                                                 {                                                
                                                     uow.CheckRunRepository.AddCheckRun(
                                                         new CheckRun
                                                         {
                                                             CheckId = currentData.Id,
                                                             Status=data.Result.Item1,
                                                             RunTime=data.Result.Item2.ToString(),
                                                             LastRunOn=DateTime.UtcNow
                                                         });
                                                     return data.Result.Item1;
                                                 } ).ContinueWith((status)=>
                                                 {
                                                     if (status.Result != UrlStatus.UP.ToString())
                                                         SendEmail();
                                                 
                                                 }));
                        taskexecute.Wait();                       
                        _logger.LogInformation($"----url execution End---");
                    }                
                    
                }

                if(result.Any() && context.JobDetail.Key.Name == "MonitorMainJob")
                {
                    //add new job or start existing
                  foreach (var item in result.Where(x => (x.IsActive && !x.IsScheduled) || (x.IsActive && x.IsScheduled && (!context.Scheduler.CheckExists(new JobKey($"{x.Id}")).Result))))
                   {
                        _logger.LogInformation($"{context.JobDetail.Key}--created---");
                        schedule.ScheduleJob(CreateJob(_jobSchedules, item), CreateTrigger(_jobSchedules, item));
                        uow.CheckRegisterRepository.UpdateCheckSchedule(item.Id, true);
                   }

                    //delete disable  job
                    foreach (var item in result.Where(x => !x.IsActive && !x.IsScheduled).ToList())
                    {
                        _logger.LogInformation($"{context.JobDetail.Key}--Deleted---");
                        schedule.DeleteJob(new JobKey($"{item.Id}"));

                    }
                }
                uow.Commit();
            }

            
            _logger.LogInformation($"Actual fire {context.FireTimeUtc} and next {context.NextFireTimeUtc}");
            _logger.LogInformation("---------------*********------------------------------");
            return Task.CompletedTask;
        }
        private static IJobDetail CreateJob(JobSchedule schedule, CheckRegister item)
        {
          
            return JobBuilder
                .Create(schedule.JobType)
                .WithIdentity($"{item.Id}")
                .WithDescription($"Job{item.Id}-{item.Name}")
                .Build();
        }

        private  void SendEmail()
        {
            _logger.LogInformation($"The email is sent to {_appSettings.EmailTo}");
        }

        private async Task <(string , TimeSpan)> RunUrl(CheckRegister data)
        {           
            string status = string.Empty;
            var client = _httpClientFactory.CreateClient();
            try
            {
                var policy = Policy.Handle<HttpRequestException>()
                    .OrResult<HttpResponseMessage>(r=>r.StatusCode!= HttpStatusCode.OK)
                    .WaitAndRetryAsync(_appSettings.RetryAttempt, retryAttempt => TimeSpan.FromMilliseconds(3000));
               
               
                  var stopWatch = Stopwatch.StartNew();
                  var response = await policy.ExecuteAsync(async ()=>await client.GetAsync(new Uri(data.Url)));

            
                status = response.StatusCode == HttpStatusCode.OK ? UrlStatus.UP.ToString() : UrlStatus.Down.ToString();
                return (status, stopWatch.Elapsed);
                
            }
            catch (HttpRequestException)
            { 
                return (UrlStatus.BadUrl.ToString(), TimeSpan.FromMilliseconds(0));             
            }
            catch  (Exception)
            {
                return (UrlStatus.BadUrl.ToString(), TimeSpan.FromMilliseconds(0));
            }
            finally
            {
                client.Dispose();
            }
            
        }
        private static ITrigger CreateTrigger(JobSchedule schedule, CheckRegister item)
        {
            var frequency = item.FrequencyType == "mm" ? item.Frequency : item.Frequency * 60;

            return TriggerBuilder
                .Create()
                .WithIdentity($"{item.Name}{item.Id}.trigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(frequency).RepeatForever())
                .WithDescription($"the trigger {item.Name} and {schedule.JobType}")
                .Build();
        }
       
    }

}
