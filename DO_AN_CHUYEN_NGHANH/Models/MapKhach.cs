using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DO_AN_CHUYEN_NGHANH.Models
{
    public class MapKhach
    {
        public List<KhachHang> dsKhachHang()
        {
            QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1();
            var data = (from khachhang in db.KhachHangs                        
                        select khachhang).ToList();
            return data;
        }
        public bool ThemKhachHang(KhachHang model)
        {
            try
            {
                using (var db = new QUANLYPHONGTROEntities1())
                {
                   
                    var taiKhoan = db.KhachHangs.SingleOrDefault(tk => tk.TenTaiKhoan == model.TenTaiKhoan);
                    if (taiKhoan != null)
                    {                       
                        return false;
                    }                   
                       
                    db.KhachHangs.Add(model);
                       
                    db.SaveChanges();
                        
                    return true;
                     
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm khách hàng: " + ex.Message);
                return false;
            }
        }
        

    }
}