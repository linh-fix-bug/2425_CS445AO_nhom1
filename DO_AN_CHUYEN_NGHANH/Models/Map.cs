using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DO_AN_CHUYEN_NGHANH.Models
{
    public class Map

    {
        public string message = "";
        public List<Phong> DanhSachTheoTang(int? idTang)
        {
            QUANLYPHONGTROEntities1 ds = new QUANLYPHONGTROEntities1();
            var data = (from phong in ds.Phongs
                        where phong.Tang == idTang
                        select phong).ToList();
            return data;
        }
        public List<Phong> DanhSach()
        {
            QUANLYPHONGTROEntities1 ds = new QUANLYPHONGTROEntities1();
            var data = (from phong in ds.Phongs                        
                        select phong).ToList();
            return data;
        }
        public bool ThemMoi(Phong model)
        {
            // Kiểm tra dữ liệu đầu vào
            if (model == null)
            {
                message = "Dữ liệu không được để trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.GhiChu))
            {
                message = "Ghi chú không được để trống.";
                return false;
            }

            if (model.Gia <= 0)
            {
                message = "Giá phải lớn hơn 0.";
                return false;
            }                        
            try
            {
                using (var db = new QUANLYPHONGTROEntities1())
                {
                    db.Phongs.Add(model);
                    db.SaveChanges();     
                    message = "Thêm mới phòng thành công.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi từ cơ sở dữ liệu hoặc ngoại lệ khác
                message = $"Lỗi khi thêm dữ liệu: {ex.Message}";
                return false;
            }
        }
        public bool CapNhat(Phong model)
        {
            try
            {
                using (var db = new QUANLYPHONGTROEntities1())
                {
                    var updateModel = db.Phongs.Find(model.MaPhong);

                    if (updateModel == null)
                    {
                        message = "Phòng không tồn tại.";
                        return false;
                    }

                    updateModel.TenPhong = model.TenPhong;
                    updateModel.Gia = model.Gia;
                    updateModel.Tang = model.Tang;
                    updateModel.TinhTrang = model.TinhTrang;
                    updateModel.GhiChu = model.GhiChu;

                  
                    if (!string.IsNullOrEmpty(model.HinhAnh))
                    {
                        updateModel.HinhAnh = model.HinhAnh;
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Lỗi: " + ex.Message;
                return false;
            }
        }

        public Phong ChiTiet(int idPhong)
        {
            QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1();
            return db.Phongs.SingleOrDefault(item => item.MaPhong == idPhong);
        }
        public bool Xoa(int idPhong)
        {
            QUANLYPHONGTROEntities1 db = new QUANLYPHONGTROEntities1();
            var model = db.Phongs.SingleOrDefault(item => item.MaPhong == idPhong);
            if(model != null)
            {
                db.Phongs.Remove(model);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}