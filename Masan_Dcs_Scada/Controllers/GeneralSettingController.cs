using Masan_Dcs_Scada.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*general settings chứa 2 view : general settings và shiftsettings */
/*index là của general settings và shift settings là của shift settings*/

namespace Masan_Dcs_Scada.Controllers
{
    public class GeneralSettingController : Controller
    {
        DatabaseContext _db;

        public IEnumerable<HeadShift> HeadShifts { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public GeneralSettingController(DatabaseContext db)
        {
            _db = db;
            HeadShifts = new List<HeadShift>();
            Products = new List<Product>();
        }

        public IActionResult Index()
        {
            TempData["headShifts"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.HeadShifts.ToList());
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            return View();
        }


        [HttpPost]
        public IActionResult AddProduct(string productCode, string productName)
        {
            if(!Regex.IsMatch(productCode, @"^((SP)(\d+))$"))
            {
                ModelState.AddModelError("Error", "Mã Sản phẩm không đúng định dạng!");
            }
            else
            {
                var pd = (from product_ in _db.Products where product_.Code.Equals(productCode) select product_).FirstOrDefault();
                if (pd != null)
                {
                    ModelState.AddModelError("Error", "Mã Sản phẩm đã tồn tại");
                }
                else
                {
                    var product = new Product() { Code = productCode, Name = productName };
                    _db.Products.Add(product);
                    int rows = _db.SaveChanges();
                    if (rows == 1)
                    {
                        ModelState.AddModelError("Error", "Thêm sản phẩm thành công!");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Có Lỗi vui lòng thử lại");
                    }
                }
            }

            
            TempData["headShifts"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.HeadShifts.ToList());
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            return View("Index");
        }

        [HttpPost]
        public IActionResult AddHeadShift(string code, string fullName)
        {
            try
            {
                if (!Regex.IsMatch(code, @"^((TC)(\d+))$"))
                {
                    ModelState.AddModelError("Error", "Mã Trưởng ca không đúng định dạng!");
                }
                else
                {
                    var pd = (from hs in _db.HeadShifts where hs.Code.Equals(code) select hs).FirstOrDefault();
                    if (pd != null)
                    {
                        ModelState.AddModelError("Error", "Mã trưởng ca đã tồn tại");
                    }
                    else
                    {
                        var headShift = new HeadShift() { Code = code, FullName = fullName };
                        _db.HeadShifts.Add(headShift);
                        int rows = _db.SaveChanges();
                        if (rows == 1)
                        {
                            ModelState.AddModelError("Error", "Thêm trưởng ca thành công!");
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "Có Lỗi vui lòng thử lại");
                        }
                    }
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            

            TempData["headShifts"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.HeadShifts.ToList());
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            return View("Index");
        }

        [HttpPost]
        public IActionResult RemoveHeadShift()
        {
            var hs = _db.HeadShifts.First();
            _db.HeadShifts.Remove(hs);
            int rows = _db.SaveChanges();
            if(rows > 0)
            {
                ModelState.AddModelError("Error", "Đã xóa thành công!");
            }
            else
            {
                ModelState.AddModelError("Error", "Có Lỗi vui lòng thử lại!");
            }
            TempData["headShifts"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.HeadShifts.ToList());
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            return View("Index");
        }

        [HttpPost]
        public IActionResult RemoveProduct()
        {
            var hs = _db.Products.First();
            _db.Products.Remove(hs);
            _db.SaveChanges();
            TempData["headShifts"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.HeadShifts.ToList());
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            return View("Index");
        }

        public IActionResult ShiftSetting()
        {
            TempData["headShifts"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.HeadShifts.ToList());
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            if(_db.Shifts.ToList().Count == 0)
            {
                _db.Shifts.Add(new Shift() { ShiftId = 1 });
                _db.SaveChanges();
                TempData["shift"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Shifts.ToList()[0]);
            }
            else
            {
                TempData["shift"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Shifts.ToList()[0]);
            }
            return View();
        }

        /*lưu dữ liệu trưởng ca*/
        [HttpPost]
        public IActionResult UpdateShift(string headShift1, DateTime start1,
            string headShift2, DateTime start2, string headShift3, DateTime start3)
        {
            Shift shift = _db.Shifts.First();
            shift.HeadShiftCode1 = headShift1;
            shift.HeadShiftCode2 = headShift2;
            shift.HeadShiftCode3 = headShift3;
            shift.ShiftStartTime1 = start1;
            shift.ShiftStartTime2 = start2;
            shift.ShiftStartTime3 = start3;
            _db.SaveChanges();
            TempData["shift"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Shifts.ToList()[0]);
            TempData["headShifts"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.HeadShifts.ToList());
            TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
            return View("ShiftSetting");
        }
    }
}
