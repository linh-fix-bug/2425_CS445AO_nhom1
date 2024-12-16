using DO_AN_CHUYEN_NGHANH.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1();
            var danhsach = db.Phongs.ToList();
            return View(danhsach);
        }

        public ActionResult ThemPhong(Phong model, HttpPostedFileBase HinhAnh)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Dữ liệu nhập vào không hợp lệ.");
                return View(model);
            }

            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                try
                {
                    // Đường dẫn lưu ảnh
                    string fileName = Path.GetFileName(HinhAnh.FileName);
                    string path = Path.Combine(Server.MapPath("~/images"), fileName);

                    // Lưu file vào thư mục
                    HinhAnh.SaveAs(path);

                    // Gán đường dẫn ảnh cho model
                    model.HinhAnh = "/images/" + fileName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi upload ảnh: {ex.Message}");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng chọn file ảnh.");
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
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }

        public ActionResult CapNhat(int idPhong)
        {
            var map = new Map();
            var phongEdit = map.ChiTiet(idPhong);

            if (phongEdit == null)
            {
                TempData["ErrorMessage"] = "Phòng không tồn tại.";
                return RedirectToAction("DanhSachPhong");
            }

            return View(phongEdit);
        }

        [HttpPost]
        public ActionResult CapNhat(Phong model, HttpPostedFileBase HinhAnh)
        {
            var map = new Map();
            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(HinhAnh.FileName);
                    string path = Path.Combine(Server.MapPath("~/images"), fileName);

                    HinhAnh.SaveAs(path);
                    model.HinhAnh = "/images/" + fileName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi upload ảnh: {ex.Message}");
                    return View(model);
                }
            }

            if (map.CapNhat(model))
            {
                TempData["SuccessMessage"] = "Cập nhật phòng thành công!";
                return RedirectToAction("DanhSachPhong"); 
            }
            else
            {
                ModelState.AddModelError("", map.message); 
                return View(model); 
            }
        }

        public ActionResult Xoa(int idPhong)
        {
            Map map = new Map();
            map.Xoa(idPhong);
            return RedirectToAction("");
        }


    }
}