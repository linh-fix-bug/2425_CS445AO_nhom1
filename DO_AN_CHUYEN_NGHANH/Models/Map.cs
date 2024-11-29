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
            QUANLYPHONGTROEntities ds = new QUANLYPHONGTROEntities();
            var data = (from phong in ds.Phongs
                        where phong.Tang == idTang
                        select phong).ToList();
            return data;
        }
        public List<Phong> DanhSach()
        {
            QUANLYPHONGTROEntities ds = new QUANLYPHONGTROEntities();
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

            // Thực hiện thêm dữ liệu
            try
            {
                using (var db = new QUANLYPHONGTROEntities())
                {
                    db.Phongs.Add(model); // Thêm đối tượng vào DbSet
                    db.SaveChanges();     // Lưu thay đổi vào cơ sở dữ liệu
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


    }
}