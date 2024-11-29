using DO_AN_CHUYEN_NGHANH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DO_AN_CHUYEN_NGHANH.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room

        public ActionResult Index()
        {
            QUANLYPHONGTROEntities db = new QUANLYPHONGTROEntities();
            var danhsach = db.Phongs.ToList();
            return View(danhsach);
        }

        public ActionResult ThemPhong(Phong model)
        {
            // Kiểm tra dữ liệu có hợp lệ không trước khi xử lý
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Dữ liệu nhập vào không hợp lệ.");
                return View(model);
            }

            // Gọi lớp Map để thêm mới phòng
            Map map = new Map();
            if (map.ThemMoi(model))
            {
                TempData["SuccessMessage"] = "Thêm phòng thành công!";
                return View();
            }
            else
            {
                ModelState.AddModelError("", map.message); // Thêm thông báo lỗi từ Map
                return View(model);
            }
        }

    }
}