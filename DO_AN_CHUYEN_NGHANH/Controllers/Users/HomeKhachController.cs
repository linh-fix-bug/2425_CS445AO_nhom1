using DO_AN_CHUYEN_NGHANH.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DO_AN_CHUYEN_NGHANH.Controllers
{
    public class HomeKhachController : Controller
    {
        // GET: HomeKhach
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
        public ActionResult ThongTin(KhachHang model, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new QUANLYPHONGTROEntities1())
                    {
                        
                        var taiKhoan = db.KhachHangs.SingleOrDefault(tk => tk.TenTaiKhoan == model.TenTaiKhoan);
                        if (taiKhoan != null)
                        {
                            ViewBag.thongbao = "Tài khoản đã tồn tại. Vui lòng kiểm tra lại!";
                            return View("ThongTin", model); 
                        }
                        if (string.IsNullOrEmpty(model.HoVaTen) || string.IsNullOrEmpty(model.SoDienThoai) || string.IsNullOrEmpty(model.QueQuan))
                        {
                            ViewBag.thongbao = "Tất cả các trường thông tin (Họ và tên, Số điện thoại, Quê quán, tên phòng đặt) không được để trống!";
                            return View("ThongTin", model); 
                        }
                        if (HinhAnh != null && HinhAnh.ContentLength > 0)
                        {
                            
                            string fileName = Path.GetFileName(HinhAnh.FileName);
                            string path = Path.Combine(Server.MapPath("~/images"), fileName);

                            
                            HinhAnh.SaveAs(path);

                            
                            model.HinhAnh = "/images/" + fileName;
                        }

                        
                        db.KhachHangs.Add(model);
                        db.SaveChanges();

                       
                        ViewBag.thongbao = "Thêm khách hàng thành công!";
                        return RedirectToAction(""); 
                    }
                }
                catch (Exception ex)
                {
                  
                    ViewBag.thongbao = "Có lỗi xảy ra: " + ex.Message;
                }
            }
            else
            {
                
                ViewBag.thongbao = "Dữ liệu không hợp lệ!";
            }

           
            return View("ThongTin", model);
        }


        public ActionResult ThongTinNguoiDung()
        {
            return View();
        }



    }
}