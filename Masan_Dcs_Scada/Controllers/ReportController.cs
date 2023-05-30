using Masan_Dcs_Scada.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace Masan_Dcs_Scada.Controllers
{
    public class ReportController : Controller
    {
        protected DatabaseContext _db;

        public ReportController(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetReport(DateTime from, DateTime to)
        {
            // gọi lên service để lấy về list customer
            List<Quantity> quantities = _db.Quantities.Where(q => q.Date >= from && q.Date <= to).ToList();
            foreach (var quantity in quantities)
            {
                var e = _db.Entry(quantity);
                e.Reference(q => q.Product).Load();
            }

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                //Đặt tên người tạo file
                package.Workbook.Properties.Author = "ThongNT";
                //Đặt tiêu đề cho file
                package.Workbook.Properties.Title = "Báo cáo sản lượng";
                // tạo sheet 
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                // tạo fontsize và fontfamily cho sheet
                workSheet.Cells.Style.Font.Size = 11;
                workSheet.Cells.Style.Font.Name = "Calibri";

                // danh sách các tên cột
                string[] arrColumnHeader = { "STT" , "Ngày nhập","Mã sản phẩm", "Tên sản phẩm", "Số lượng",
                    "Ca", "Dây chuyền"};



                // merge các column lại từ column 1 đến số column header
                // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                workSheet.Cells[1, 1].Value = "BÁO CÁO SẢN LƯỢNG";
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Merge = true;
                // in đậm
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.Font.Bold = true;
                // căn giữa
                workSheet.Cells[1, 1, 1, arrColumnHeader.Count()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                workSheet.Cells[2, 1].Value = "";
                workSheet.Cells[2, 1].Style.Font.Size = 15;
                workSheet.Cells[2, 1, 2, arrColumnHeader.Count()].Merge = true;

                // Gán row header
                for (var i = 0; i < arrColumnHeader.Length; i++)
                {
                    workSheet.Cells[3, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[3, i + 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#D4D4D4"));
                    workSheet.Cells[3, i + 1].Value = arrColumnHeader[i];
                }
                // chỉnh style cho bảng
                workSheet.Row(3).Style.Font.Bold = true;
                workSheet.Row(3).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(3).Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#D8D8D8"));
                workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Chỉnh độ rộng các cột 
                workSheet.Column(1).Width = 5;
                workSheet.Column(2).Width = 25;
                workSheet.Column(3).Width = 25;
                workSheet.Column(4).Width = 25;
                workSheet.Column(5).Width = 25;
                workSheet.Column(6).Width = 25;
                workSheet.Column(7).Width = 25;

                // Gán data list vào sheet
                var rowIndex = 4;
                foreach (var quantity in quantities)
                {
                    workSheet.Cells[rowIndex, 1].Value = rowIndex - 1;
                    workSheet.Cells[rowIndex, 2].Value = quantity.Date == null ? "" : quantity.Date.ToString("dd/MM/yyyy");
                    workSheet.Cells[rowIndex, 3].Value = quantity.ProductCode;
                    workSheet.Cells[rowIndex, 4].Value = quantity.Product.Name;
                    workSheet.Cells[rowIndex, 5].Value = quantity.ProductNumber;
                    workSheet.Cells[rowIndex, 6].Value = "Ca " + quantity.Shift;
                    workSheet.Cells[rowIndex, 7].Value = "Line " + quantity.Line;
                    rowIndex++;
                }
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"Report-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            string dir = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/report");
            string filePath = Path.Combine(dir, excelName);
            FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            stream.WriteTo(file);
            file.Close();
            //stream.Close();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
