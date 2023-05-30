using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Masan_Dcs_Scada.Models
{
    public class Product
    {
        [Key]
        [Required]
        [StringLength(20)]
        [Display(Name = "Mã Sản Phẩm")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
    }
}
