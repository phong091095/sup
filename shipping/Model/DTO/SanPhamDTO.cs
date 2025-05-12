namespace shipping.Model.DTO
{
    public class SanPhamDTO
    {
        public string IDSanPham { get; set; } = default!;

        public int IDDanhMuc { get; set; }

        public string IDCuaHang { get; set; }
        public string TenSanPham { get; set; }
        public byte[] HinhAnhChinh { get; set; }
        public string? MoTa { get; set; }
        public string? TrangThai { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}
