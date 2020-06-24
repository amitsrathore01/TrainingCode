using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XupApi.Models
{
    public class CheckRegisterModel
    {
        public string Name { get; set; }       
        public string Url { get; set; }
        public bool IsActive { get; set; }      
        public string FrequencyType { get; set; }
        public int Frequency { get; set; }          
        public DateTime CreatedOn { get; set; }

    }
}
