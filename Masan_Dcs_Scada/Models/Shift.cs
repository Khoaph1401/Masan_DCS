using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Masan_Dcs_Scada.Models
{
    public class Shift
    {
        [Key]
        public int ShiftId { get; set; }

        public string HeadShiftCode1 { get; set; }

        [ForeignKey("HeadShiftCode1")]
        public HeadShift HeadShift1 { get; set; }

        public DateTime ShiftStartTime1 { get; set; }

        public string HeadShiftCode2 { get; set; }

        [ForeignKey("HeadShiftCode2")]
        public HeadShift HeadShift2 { get; set; }

        public DateTime ShiftStartTime2 { get; set; }

        public string HeadShiftCode3 { get; set; }

        [ForeignKey("HeadShiftCode3")]
        public HeadShift HeadShift3 { get; set; }

        public DateTime ShiftStartTime3 { get; set; }
    }
}
