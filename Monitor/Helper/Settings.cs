using System;
using System.Collections.Generic;
using System.Text;

namespace XupMonitor.Helper
{
    public class AppSettings 
    {
        public int RetryAttempt { get; set; }
        public string EmailTo { get; set; }

    }
    public class ConnectionStrings
    {
        public string DbXubConString { get; set; }
    }
}
