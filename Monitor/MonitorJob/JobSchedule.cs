using System;
using System.Collections.Generic;
using System.Text;

namespace XupMonitor
{
    public class JobSchedule
    {
        public JobSchedule(Type jobType)
        {
            JobType = jobType;
        }

        public Type JobType { get; }

    }
}
