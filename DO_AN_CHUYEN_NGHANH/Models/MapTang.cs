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
            QUANLYPHONGTROEntities db = new QUANLYPHONGTROEntities(); 
            return db.Tangs.ToList();
        }
    }
}