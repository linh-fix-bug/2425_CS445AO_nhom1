using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DO_AN_CHUYEN_NGHANH.Models
{
    public class Map
    {
        public List<Phong> DanhSachTheoTang(int? idTang)
        {
            QUANLYPHONGTROEntities ds = new QUANLYPHONGTROEntities();
            var data = (from phong in ds.Phongs
                        where phong.Tang == idTang
                        select phong).ToList();
            return data;
        }

    }
}