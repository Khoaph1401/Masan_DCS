using Masan_Dcs_Scada.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masan_Dcs_Scada.Controllers
{
    public class MonitoringController : Controller
    {
        private DatabaseContext _db;

        public MonitoringController(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            TempData["systemAttributes"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.SystemAttributes.ToList());
            TempData["quantities"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Quantities.ToList());
            return View();
        }
    }
}
