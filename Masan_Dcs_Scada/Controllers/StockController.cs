using Masan_Dcs_Scada.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masan_Dcs_Scada.Controllers
{
    public class StockController : Controller
    {
        DatabaseContext _db;

        public StockController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            return View();
        }

        [HttpPost]
        public IActionResult InsertStock(DateTime date, string productCode, string shift, string line, int quantity)
        {
            Quantity quantity1 = new Quantity()
            {
                Date = date,
                ProductCode = productCode,
                Shift = Int32.Parse(shift.Replace("shift", "")),
                Line = Int32.Parse(line.Replace("line", "")),
                ProductNumber = quantity
            };
            _db.Quantities.Add(quantity1);
            _db.SaveChanges();
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            string r = HttpContext.Session.GetString("role");
            return View("Index");
        }
    }
}
