using DO_AN_CHUYEN_NGHANH.Controllers;
using DO_AN_CHUYEN_NGHANH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DO_AN_CHUYEN_NGHANH.Controllers
{
    public class DangKiController : Controller
    {
        // GET: DangKi
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string tenDangKi, string matKhau, string nhapLaiMatKhau)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(tenDangKi))
            {
                ViewBag.thongbao = "Không được để rỗng";
                return View();
            }
            if (!tenDangKi.EndsWith("@gmail.com"))
            {
                ViewBag.thongbao = "Vui lòng đăng ký bằng định dạng @gmail.com!";
                return View();
            }
            if (string.IsNullOrEmpty(matKhau) || matKhau.Length < 6)
            {
                ViewBag.thongbao = "Mật khẩu phải có ít nhất 6 ký tự.";

                return View();
            }
            if (matKhau != nhapLaiMatKhau)
            {
                ViewBag.thongbao = "Mật khẩu nhập lại không trùng khớp.";

                return View();
            }

            // Gọi hàm đăng ký
            var result = new mapTaiKhoan().DangKi(tenDangKi, matKhau);
            if (!result) // Nếu đăng ký thất bại
            {
                ViewBag.thongbao = "Đăng ký thất bại. Email có thể đã tồn tại hoặc có lỗi hệ thống.";
                return View();
            }

            // Đăng ký thành công
            TempData["SuccessMessage"] = "Đăng ký thành công. Vui lòng đăng nhập.";
            return RedirectToAction("Index", "Login");
        }


    }
}
