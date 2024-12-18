using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using DO_AN_CHUYEN_NGHANH.Models;

namespace DO_AN_CHUYEN_NGHANH.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1();
            var danhsachPhong = db.Phongs.ToList();
            return View(danhsachPhong);
        }

        public ActionResult Timkiem(int? idTang, int? idGia)
        {
            ViewBag.idTang = idTang;
            ViewBag.idGia = idGia;

            Map tang = new Map();
            var danhSachPhong = tang.DanhSach();


            if (idTang != null && idTang != 0)
            {
                danhSachPhong = danhSachPhong.Where(p => p.Tang == idTang).ToList();
            }

            if (idGia != null && idGia != 0)
            {
                switch (idGia)
                {
                    case 1: // 1-2 triệu
                        danhSachPhong = danhSachPhong.Where(p => p.Gia >= 1000000 && p.Gia < 2000000).ToList();
                        break;
                    case 2: // 2-3 triệu
                        danhSachPhong = danhSachPhong.Where(p => p.Gia >= 2000000 && p.Gia < 3000000).ToList();
                        break;
                    case 3: // 3-4 triệu
                        danhSachPhong = danhSachPhong.Where(p => p.Gia >= 3000000 && p.Gia <= 4000000).ToList();
                        break;
                }
            }

            return View(danhSachPhong); // Trả về danh sách phòng sau khi lọc
        }
        public ActionResult XemChiTiet(int? maphong)
        {
            if (maphong == null)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn một phòng để xem chi tiết!";
                return RedirectToAction("DanhSachPhong", "Home");
            }

            using (var db = new QUANLYPHONGTROEntities1())
            {
                var phong = db.Phongs.FirstOrDefault(p => p.MaPhong == maphong.Value);

                if (phong == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy phòng có mã: " + maphong;
                    return RedirectToAction("DanhSachPhong", "Home");
                }

                return View(phong);
            }
        }

    }
}