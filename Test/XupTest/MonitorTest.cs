//using Microsoft.Extensions.Logging;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using XupMonitor.Helper;
//using XupMonitor;
//using Quartz;
//using Microsoft.Extensions.Options;
//using System.Threading;
//using Quartz.Spi;
//using Quartz.Impl;

//namespace XupTest
//{
//    [TestClass]
//   public class MonitorTest
//    {
//        Mock<ISchedulerFactory> _schedulerFactory;
//        Mock<ILogger<MonitorJob>> _logger;
//        Mock<JobSchedule> _jobSchedules;
//        Mock<ConnectionStrings> _config;
//        Mock<AppSettings> _appSettings;
//        Mock<IHttpClientFactory> _httpClientFactory;
//        Mock<IJobFactory> _jobFactory;
//        Mock<JobDetailImpl> _jobDetail;
//        Mock<IScheduler> _objScheduler;
//        AppSettings appSettings = new AppSettings() {RetryAttempt=3};
//        ConnectionStrings connString = new ConnectionStrings() { DbXubConString = "Data Source=DESKTOP-M7N5B67\\SQLEXPRESS;Initial Catalog=Xup;Integrated Security=True" };
//        public MonitorTest()
//        {
           

//            _logger =  new Mock<ILogger<MonitorJob>>();
//            _jobSchedules = new Mock<JobSchedule>(typeof(MonitorJob));             
//            _httpClientFactory = new Mock<IHttpClientFactory>();
//            _config = new Mock<ConnectionStrings>();
//            _appSettings = new Mock<AppSettings>();
//            _schedulerFactory = new Mock<ISchedulerFactory>();
//            _jobFactory = new Mock<IJobFactory>();
//            _jobDetail = new Mock<JobDetailImpl>();
//            _objScheduler = new Mock<IScheduler>();
//        }
//       // public IScheduler Scheduler { get; set; }
//        [TestMethod]
//        public void TestMonitorJobExecute()
//        {

//            IOptions<AppSettings> optionsAppSettings = Options.Create(appSettings);
//            IOptions<ConnectionStrings> optionsConnString = Options.Create(connString);
//            var objMonitor = new MonitorJob(_logger.Object,
//                                           _jobSchedules.Object,
//                                          optionsConnString.Value,
//                                          optionsAppSettings.Value, 
//                                           _httpClientFactory.Object);

//            var objJobExecu = new Mock<IJobExecutionContext>();

//            //objJobExecu.SetupGet(p => p.JobDetail.Key).Returns(new JobKey("Name", "MonitorMainJob"));
//            //objJobExecu.SetupGet(p => p.JobDetail.Description).Returns("MonitorMainJob");
//            //objJobExecu.SetupGet(p => p.JobDetail).Returns(_jobDetail.Object);



//            var cancellationToken = CancellationToken.None;
           


//           // _objScheduler.Setup(x=>x.JobFactory.ReturnJob(_schedulerFactory.Object.GetScheduler().Result))

//           // Scheduler.JobFactory = _jobFactory.Setup(x=>x.);

//            var job = CreateJob(_jobSchedules.Object);
//            var trigger = CreateTrigger(_jobSchedules.Object);
//         //   _objScheduler.Setup(x => x.ScheduleJob(job, trigger).Result);


//            _objScheduler.Object.ScheduleJob(job, trigger);
//            _objScheduler.Object.Start();

//            // objJobExecu.Setup(x => x.Scheduler = Scheduler);

//            objJobExecu.Setup(x => x.Scheduler).Returns(_objScheduler.Object);
//            objMonitor.Execute(objJobExecu.Object);

//        }

//        private static ITrigger CreateTrigger(JobSchedule schedule)
//        {

//            return TriggerBuilder
//                .Create()
//                .WithIdentity($"Monitor.trigger")
//                .StartNow()
//                .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever())
//                .WithDescription($"the trigger Main")
//                .Build();
//        }
//        private static IJobDetail CreateJob(JobSchedule schedule)
//        {
//            return JobBuilder
//                .Create(schedule.JobType)
//                .WithIdentity($"MonitorMainJob")
//                .WithDescription($"MonitorMainJob")
//                .Build();
//        }
//    }
//}
