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

            if (string.IsNullOrEmpty(tenDangNhap) && string.IsNullOrEmpty(matKhau))
            {
                return View();
            }
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.thongbao = "Nhập đầy đủ thông tin";
                return View();
            }

            var taikhoan = new mapTaiKhoan().chitiet(tenDangNhap);


            if (taikhoan == null)
            {
                ViewBag.thongbao = "Tài khoản hoặc mật khẩu không đúng";
                ViewBag.tenDangNhap = tenDangNhap;
                return View();
            }


            if (matKhau != taikhoan.MatKhau)
            {
                ViewBag.thongbao = "Tài khoản hoặc mật khẩu không đúng";
                ViewBag.tenDangNhap = tenDangNhap;
                return View();
            }

            Session["user"] = taikhoan;

            if (tenDangNhap.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                return Redirect("/HomeAdmin");
            }
            else
            {
                return Redirect("/HomeKhach");
            }
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }





    }
}