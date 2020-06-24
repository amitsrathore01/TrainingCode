using System;
using System.Collections.Generic;
using System.Text;

namespace XupMonitor.DataAccess.Entities
{
    public class CheckRegister    {
        public  Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public string FrequencyType { get; set; }
        public int Frequency { get; set; }
        public bool IsScheduled { get; set; }
    }
}
