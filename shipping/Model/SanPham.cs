using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class SanPham
    {
        [Key]
        public string IDSanPham {  get; set; } = default!;
        public int IDDanhMuc { get; set; }
        public int IDPhanLoaiDanhMuc { get; set; }
        public string IDCuaHang { get; set; }
        public string TenSanPham { get; set; }
        public byte[] HinhAnhChinh { get; set; }
        public string? MoTa {  get; set; }
        public string? TrangThai {  get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}
