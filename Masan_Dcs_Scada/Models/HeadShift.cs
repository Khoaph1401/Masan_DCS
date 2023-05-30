using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Masan_Dcs_Scada.Models
{
    public class HeadShift 
    {
        [Key]
        [Required]
        [StringLength(20)]
        [Display(Name = "Mã trưởng ca")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }
    }
}
