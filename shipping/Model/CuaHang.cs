using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class CuaHang
    {
        [Key]
        public string IDCuaHang { get; set; } = default!;
        public int ID { get; set; } = default!;
        public string TenCuaHang { get; set; } = default!;
        public string DiaChi { get; set; } = default!;
        public string TrangThai { get; set; } = default!;
        public DateTime NgayDangKy { get; set; } 
    }
}
