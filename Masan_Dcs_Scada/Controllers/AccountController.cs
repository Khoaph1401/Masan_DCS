using Masan_Dcs_Scada.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Masan_Dcs_Scada.Controllers
{
    public class AccountController : Controller
    {

        DatabaseContext _db;
        public AccountController(DatabaseContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Account account)
        {
            /*câu lệnh truy vấn từ cơ sở dữ liệu*/
            Account acc = (from a in _db.Accounts
                           where a.Name.Equals(account.Name)
                           && a.Password.Equals(account.Password)
                           select a).FirstOrDefault();

            if (acc != null)
            {
                /*TempData : truyền dữ liệu từ controller sang view -  1 biến global*/
                TempData["headShifts"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.HeadShifts.ToList());
                TempData["products"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Products.ToList());
                string _role = acc.Role ? "admin" : "operator";
                HttpContext.Session.SetString("role", _role);
                
                if(_role == "admin")
                {
                    return RedirectToAction("Index", "GeneralSetting");
                }
                else
                {
                    TempData["shift"] = Newtonsoft.Json.JsonConvert.SerializeObject(_db.Shifts.ToList()[0]);
                    return RedirectToAction("ShiftSetting", "GeneralSetting");
                }
                
            }
            else
            {
                /* thuộc tính có sẵn của MVC: tempdata, viewdata*/
                ModelState.AddModelError("Error", "Tài khoản không đúng!");
                return View();
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePassword(string username, string oldPassword, string newPassword, string _newPassword)
        {
            //if ((String.IsNullOrWhiteSpace(username)) && (String.IsNullOrWhiteSpace(oldPassword))
            //    &&(String.IsNullOrWhiteSpace(newPassword)) &&(String.IsNullOrWhiteSpace(_newPassword)))
            //{
            //    ModelState.AddModelError("Error", "");
            //}
            if (!newPassword.Equals(_newPassword))
            {
                ModelState.AddModelError("Error", "Mật khẩu nhập lại phải trùng nhau");
            }
            else
            {
                Account acc = (from a in _db.Accounts
                               where a.Name.Equals(username)
                               && a.Password.Equals(oldPassword)
                               select a).FirstOrDefault();
                if (acc != null)
                {
                    acc.Password = newPassword;
                    int rows = _db.SaveChanges();
                    if (rows > 0)
                    {
                        ModelState.AddModelError("Error", "Đổi mật khẩu thành công!");
                        return View("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Đổi mật khẩu không thành công!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Tài khoản, mật khẩu không đúng!");
                }
            }
            return View("ChangePassword");
        }
    }
}
