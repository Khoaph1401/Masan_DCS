using Masan_Dcs_Scada.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masan_Dcs_Scada.Controllers
{
    public class SettingParameterController : Controller
    {
        DatabaseContext _db;

        public SettingParameterController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            TempData["role"] = MyStorage.role;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateSetting(string line, string merchine, string shift, string productCode, int speed,
            int time1, int time2, int time3, int time4, int time5)
        {
            int _shift = Int32.Parse(shift.Replace("shift", ""));
            int _line = Int32.Parse(line.Replace("line", ""));
            int _merchine = Int32.Parse(merchine.Replace("merchine", ""));

            var query = from sa in _db.SystemAttributes where sa.Line == _line && sa.Machine == _merchine select sa;
            SystemAttribute remove_ = query.FirstOrDefault();
            if (remove_ != null)
                _db.SystemAttributes.Remove(remove_);
            SystemAttribute systemAttribute = new SystemAttribute()
            {
                Line = _line,
                Machine = _merchine,
                Shift = _shift,
                ProductCode = productCode,
                StandardSpeed = speed,
                Time1 = time1,
                Time2 = time2,
                Time3 = time3,
                Time4 = time4,
                Time5 = time5
            };
            _db.SystemAttributes.Add(systemAttribute);
            _db.SaveChanges();
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            TempData["role"] = MyStorage.role;
            return View("Index");
        }
    }
}
