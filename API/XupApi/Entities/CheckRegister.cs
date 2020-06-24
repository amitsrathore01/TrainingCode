using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XupApi.Entities
{
    public class CheckRegister
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string FrequencyType { get; set; }

        [Required]
        public int Frequency { get; set; }
        public bool IsScheduled { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
