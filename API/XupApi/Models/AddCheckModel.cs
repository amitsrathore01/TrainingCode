using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XupApi.Models
{
    public class AddCheckModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Do not enter more than 2 characters format eg: hh or mm")]
        public string FrequencyType { get; set; }

        [Required]
        [Range(1,59,ErrorMessage ="The 'mm' type should be between 1 to 59 and 'hh' should be between '1 to 24'")]
        public int Frequency { get; set; }
       
    }
}
