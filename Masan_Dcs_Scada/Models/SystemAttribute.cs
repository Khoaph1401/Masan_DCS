using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Masan_Dcs_Scada.Models
{
    public class SystemAttribute
    {
        [Key]
        public int Id { get; set; }

        public int Line { get; set; }

        public int Machine { get; set; }

        public int Shift { get; set; }

        public string ProductCode { get; set; }

        [ForeignKey("ProductCode")]
        public Product Product { get; set; }

        public int StandardSpeed { get; set; }

        public int Time1 { get; set; }

        public int Time2 { get; set; }

        public int Time3 { get; set; }

        public int Time4 { get; set; }

        public int Time5 { get; set; }
    }
}
