using DO_AN_CHUYEN_NGHANH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DO_AN_CHUYEN_NGHANH.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: HomeAdmin
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

            return View(danhSachPhong); 
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
        public ActionResult ThongTinKH()
        {           
            QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1();
            var danhsach = db.KhachHangs.ToList();
            return View(danhsach);
        }
        public ActionResult QuanLyTaiKhoan()
        {
            QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1();
            var danhsach = db.TaiKhoans.ToList();
            return View(danhsach);
        }
        [HttpGet]
        public ActionResult ThemTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemTaiKhoan(TaiKhoan model)
        {
            if (ModelState.IsValid)
            {
                var result = new mapTaiKhoan().DangKi(model.TenTaiKhoan, model.MatKhau);
                if (result)
                {
                    TempData["SuccessMessage"] = "Đăng ký thành công. Vui lòng đăng nhập.";
                    return RedirectToAction("QuanLyTaiKhoan", "HomeAdmin");
                }
                else
                {
                    TempData["ErrorMessage"] = "Đăng ký thất bại. Tên tài khoản có thể đã tồn tại hoặc có lỗi hệ thống.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.";
            }

            return View(model);
        }
        public ActionResult SuaTaiKhoan(string tenTaiKhoan)
        {
            var map = new mapTaiKhoan();
            var taiKhoan = map.LayTaiKhoanTheoTenTaiKhoan(tenTaiKhoan);

            if (taiKhoan == null)
            {
                return RedirectToAction("QuanLyTaiKhoan");
            }

            return View(taiKhoan);
        }
        [HttpPost]
        public ActionResult SuaTaiKhoan(TaiKhoan model)
        {
            if (ModelState.IsValid)
            {
                var map = new mapTaiKhoan();
                bool result = map.CapNhatTaiKhoan(model); 

                if (result)
                {
                    ViewBag.thongbao = "Cập nhật tài khoản thành công."; 
                }
                else
                {
                    ViewBag.thongbao = "Cập nhật tài khoản thất bại.";
                }

                return RedirectToAction("QuanLyTaiKhoan");
            }
            return View(model);
        }
        public ActionResult Xoa(string tenTaiKhoan)
        {
            var map = new mapTaiKhoan();
            bool result = map.Xoa(tenTaiKhoan); 

            if (result)
            {
                TempData["SuccessMessage"] = "Xóa tài khoản thành công."; 
            }
            else
            {
                TempData["ErrorMessage"] = "Xóa tài khoản thất bại."; 
            }

            return RedirectToAction("QuanLyTaiKhoan"); 
        }



    }
}