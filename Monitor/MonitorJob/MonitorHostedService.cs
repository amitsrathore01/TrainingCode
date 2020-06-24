using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;

namespace XupMonitor
{
    public class MonitorHostedService :IHostedService
    {
        private readonly ILogger<MonitorHostedService> _logger;

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly JobSchedule _jobSchedules;
        //private readonly IEnumerable<JobSchedule> _jobSchedules;
        public MonitorHostedService(
            ILogger<MonitorHostedService> logger, 
            ISchedulerFactory schedulerFactory,
            JobSchedule jobSchedules,
            IJobFactory jobFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }
        public IScheduler Scheduler { get; set; }
        public  async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;            
          
            var job = CreateJob(_jobSchedules);
            var trigger = CreateTrigger(_jobSchedules);
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);
          
        }
  

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
           
            return TriggerBuilder
                .Create()
                .WithIdentity($"Monitor.trigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever())
                .WithDescription($"the trigger Main")
                .Build();
        }
        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            return JobBuilder
                .Create(schedule.JobType)
                .WithIdentity($"MonitorMainJob")
                .WithDescription($"MonitorMainJob")
                .Build();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
    }
}
