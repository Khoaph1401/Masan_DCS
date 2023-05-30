using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Masan_Dcs_Scada.Models
{
    public class Quantity
    {
        [Key]
        public int QuantityId { get; set; }

        public DateTime Date { get; set; }

        public string ProductCode { get; set; }

        [ForeignKey("ProductCode")]
        public Product Product { get; set; }

        public int Shift { get; set; }

        public int Line { get; set; }

        public int ProductNumber { get; set; }
    }
}
