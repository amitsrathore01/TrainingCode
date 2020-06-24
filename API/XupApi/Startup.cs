using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XupApi.Helpers;
using AutoMapper;
using XupApi.Services;
using Microsoft.OpenApi.Models;

namespace XupApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

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

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<DataContext>();

            services.AddScoped<ICheckRegisterService, CheckRegisterService>();

            services.AddSwaggerGen(c =>
            {
               // c.SwaggerDoc("Xup API", new OpenApiInfo { Title = "Xup API", Version="v1"});
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Xup API", Version = "v1" });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            // migrate any database changes on startup (includes initial db creation)
            dataContext.Database.Migrate();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Xup Api");

            });

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

           
        }
    }
}
