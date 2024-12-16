using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services.Description;
using DO_AN_CHUYEN_NGHANH.Models;

namespace DO_AN_CHUYEN_NGHANH.Models
{
    public class mapTaiKhoan
    {
        public TaiKhoan chitiet(string tenDangNhap)
        {
            try
            {
               
                using (QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1())
                {                    
                    var model = db.TaiKhoans.SingleOrDefault(m => m.TenTaiKhoan.ToLower() == tenDangNhap.ToLower());
                    return model; 
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Lỗi khi truy vấn tài khoản: " + ex.Message);
                return null; 
            }
        }
        public TaiKhoan LayTaiKhoanTheoTenTaiKhoan(string tenTaiKhoan)
        {
            try
            {
                using (var db = new QUANLYPHONGTROEntities1())
                {
                    return db.TaiKhoans.FirstOrDefault(t => t.TenTaiKhoan == tenTaiKhoan); // Lấy tài khoản theo TenTaiKhoan
                }
            }
            catch
            {
                return null; // Trả về null nếu có lỗi
            }
        }


        public bool DangKi(string tenDangKi, string matKhau)
            {
                try
                {
                    using (var db = new QUANLYPHONGTROEntities1())
                    {
                        
                        if (db.TaiKhoans.Any(t => t.TenTaiKhoan == tenDangKi))
                        {
                            return false;
                        }
                        var taiKhoan = new TaiKhoan
                        {
                            TenTaiKhoan = tenDangKi,
                            MatKhau = matKhau,
                            
                        };
                        db.TaiKhoans.Add(taiKhoan);
                        db.SaveChanges();

                        return true; 
                    }
                }
                catch
                {
                    return false; 
                }
            }     
        public List<TaiKhoan> DSTaiKhoan()
        {
            QUANLYPHONGTROEntities1 ds = new QUANLYPHONGTROEntities1();
            var data = (from taikhoan in ds.TaiKhoans                        
                        select taikhoan).ToList();
            return data;
        }
        public bool CapNhatTaiKhoan(TaiKhoan model)
        {
            try
            {
                using(var db = new QUANLYPHONGTROEntities1())
                {
                    var update = db.TaiKhoans.Find(model.TenTaiKhoan);
                    if (update == null)
                    {
                        return false;
                    }
                    update.TenTaiKhoan = model.TenTaiKhoan;
                    update.MatKhau = model.MatKhau; 

                    db.SaveChanges();
                    return true;    
                }
            }catch (Exception ex)
            {               
                return false;
            }
        }

        public bool Xoa(string tenTaiKhoan)
        {
            try
            {
                using (var db = new QUANLYPHONGTROEntities1())
                {
                    // Tìm tài khoản theo tên tài khoản
                    var model = db.TaiKhoans.SingleOrDefault(item => item.TenTaiKhoan == tenTaiKhoan);
                    if (model != null)
                    {
                        db.TaiKhoans.Remove(model); // Xóa tài khoản
                        db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                        return true; // Trả về true nếu xóa thành công
                    }
                    else
                    {
                        return false; // Trả về false nếu không tìm thấy tài khoản
                    }
                }
            }
            catch (Exception)
            {
                return false; // Trả về false nếu có lỗi xảy ra
            }
        }

    }
}

