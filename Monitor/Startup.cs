using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using XupMonitor.DataAccess.Repositories;

using XupMonitor.Helper;

namespace XupMonitor
{
   public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ConnectionStrings();
            Configuration.Bind("ConnectionStrings", config);      
            services.AddSingleton(config);

            var appSetting = new AppSettings();
            Configuration.Bind("AppSettings", appSetting);
            services.AddSingleton(appSetting);

            services.AddHttpClient();
            //services.AddHttpClient<MonitorJob>("HttpClient").AddPolicyHandler(GetRetryPolicy(appSetting.RetryAttempt));

            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHostedService<MonitorHostedService>();

           services.AddSingleton<MonitorJob>();

           services.AddSingleton(new JobSchedule(jobType: typeof(MonitorJob)));

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryAttempt)
        {          
            return HttpPolicyExtensions
              // Handle HttpRequestExceptions, 408 and 5xx status codes
              .HandleTransientHttpError()

              .OrResult(msg=>msg.StatusCode==System.Net.HttpStatusCode.Forbidden)

               .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
              // Handle 404 not found
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              // Handle 401 Unauthorized
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
              // What to do if any of the above erros occur:
              // Retry 3 times, each time wait 1,2 and 4 seconds before retrying.
              .WaitAndRetryAsync(retryAttempt, retryAttempt => TimeSpan.FromMilliseconds(3000));
        }
    }
}
