using DO_AN_CHUYEN_NGHANH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;

namespace DO_AN_CHUYEN_NGHANH.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                // Chỉ gán thông báo nếu có thiếu thông tin
                ViewBag.thongbao = "Hãy điền đầy đủ thông tin";
                return View();
            }

            // Tìm tài khoản trong cơ sở dữ liệu
            var taikhoan = new mapTaiKhoan().chitiet(tenDangNhap);

            // Kiểm tra sự tồn tại của tài khoản
            if (taikhoan == null)
            {
                ViewBag.thongbao = "Tài khoản hoặc mật khẩu không đúng";
                ViewBag.tenDangNhap = tenDangNhap;
                return View();
            }

            // Kiểm tra mật khẩu
            if (matKhau != taikhoan.MatKhau)
            {
                ViewBag.thongbao = "Tài khoản hoặc mật khẩu không đúng";
                ViewBag.tenDangNhap = tenDangNhap;
                return View();
            }

            // Kiểm tra trạng thái tài khoản (nếu cần)
            /* if (taikhoan.Active != true)
             {
                 ViewBag.thongbao = "Tài khoản chưa được kích hoạt";
                 return View();
             }*/

            // Đăng nhập thành công, lưu thông tin vào session
            Session["user"] = taikhoan;

            /*if (tenDangNhap.ToLower() == "admin")
            {
                // Chuyển hướng đến trang quản trị dành cho admin
                return Redirect("/HomeAdmin");
            }
            else
            {
                // Chuyển hướng đến trang dành cho khách
                return Redirect("/HomeKhach");
            }*/
            return Redirect("/Home");

        }

       

    }
}