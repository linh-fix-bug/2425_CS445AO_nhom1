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
        public ActionResult ThemPhong()
        {
            return View();
        }
    }
}