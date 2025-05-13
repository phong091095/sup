namespace shipping.Model.DTO
{
    public class SanPhamDTO
    {
        public string  PathDanhMuc { get; set; }
        public string? IDCuaHang { get; set; }
        public string? TenSanPham { get; set; }
        public string? MoTa { get; set; }
        public string? TrangThai { get; set; }
        public DateTime? NgayTao { get; set; } = DateTime.Now;
    }
}
