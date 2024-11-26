using System;
using System.Linq;
using DO_AN_CHUYEN_NGHANH.Models;

namespace DO_AN_CHUYEN_NGHANH.Models
{
    public class mapTaiKhoan
    {
        public TaiKhoan chitiet(string tenDangNhap)
        {
            try
            {
                // Khởi tạo db context
                using (QUANLYPHONGTROEntities db = new QUANLYPHONGTROEntities())
                {
                    // Truy vấn tài khoản bằng TenTaiKhoan (chuyển đổi về chữ thường để không phân biệt hoa thường)
                    var model = db.TaiKhoans.SingleOrDefault(m => m.TenTaiKhoan.ToLower() == tenDangNhap.ToLower());

                    return model;  // Trả về tài khoản tìm thấy hoặc null nếu không tìm thấy
                }
            }
            catch (Exception ex)
            {
                // In ra lỗi chi tiết nếu có (để tiện cho việc gỡ lỗi)
                // Bạn có thể sử dụng một hệ thống logging thay vì chỉ return null
                Console.WriteLine("Lỗi khi truy vấn tài khoản: " + ex.Message);
                return null; // Trả về null nếu có lỗi
            }
        }
    }
}
