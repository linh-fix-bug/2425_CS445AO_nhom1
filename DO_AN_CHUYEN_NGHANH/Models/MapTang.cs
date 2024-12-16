using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DO_AN_CHUYEN_NGHANH.Models
{
    public class MapTang
    {
        public List<Tang> dsTang()
        {
            QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1(); 
            return db.Tangs.ToList();
        }
    }
}