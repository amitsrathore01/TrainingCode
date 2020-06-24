using System;
using System.Collections.Generic;
using System.Text;

namespace XupMonitor.DataAccess.Entities
{
  public  class CheckRun
    {
        public Guid Id { get; set; }
        public Guid CheckId { get; set; }
        public string Status { get; set; }
        public string RunTime { get; set; }
        public DateTime LastRunOn { get; set; }
        
    }
}
