using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XupApi.Entities;

namespace XupApi.Helpers
{
   
    public class DataContext:DbContext
    {
        protected readonly IConfiguration Configuration;
        readonly ConnectionStrings _config;
        public DataContext(ConnectionStrings config)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(_config.DbXubConString);          
        }
        public DbSet<CheckRegister> CheckRegister { get; set; }

        public DbSet<CheckRun> CheckRun { get; set; }
    }
}
