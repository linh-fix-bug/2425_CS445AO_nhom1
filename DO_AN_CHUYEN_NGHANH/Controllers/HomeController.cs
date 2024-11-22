using System;
using System.Collections.Generic;
using System.Linq;
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
            QUANLYPHONGTROEntities db = new QUANLYPHONGTROEntities();
            var danhsachPhong = db.Phongs.ToList();
            return View(danhsachPhong);
        }
        
        public ActionResult Timkiem(int? idTang)
        {
            ViewBag.idTang = idTang;    
            Map tang = new Map();
            return View(tang.DanhSachTheoTang(idTang));
        }
       
    }
}