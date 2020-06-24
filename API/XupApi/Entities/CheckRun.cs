using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace XupApi.Entities
{
    public class CheckRun
    {
        [Key]
        public Guid Id { get; set; }       
        public string Status { get; set; }
        public string RunTime { get; set; }
        public DateTime LastRunOn { get; set; }

        [ForeignKey("CheckId")]
        public virtual CheckRegister CheckRegister { get; set; }
    }
}
