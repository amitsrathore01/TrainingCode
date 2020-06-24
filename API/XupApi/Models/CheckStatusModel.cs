using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XupApi.Models
{
    public class CheckStatusModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
        public DateTime StatusSince { get; set; }
        public string AveragResoponseTime { get; set; }

    }
}
