using DO_AN_CHUYEN_NGHANH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DO_AN_CHUYEN_NGHANH.Controllers.Users
{
    public class RoomUsersController : Controller
    {
        // GET: RoomUsers
        public ActionResult Index()
        {
            QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1();
            var danhsach = db.Phongs.ToList();
            return View(danhsach);
        }
    }
}