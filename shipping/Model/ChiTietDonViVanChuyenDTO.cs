namespace shipping.Model
{
    public class ChiTietDonViVanChuyenDTO
    {
        // Thuộc tính từ ChiTietDVVanChuyen
        public int ID { get; set; }
        public string IDCuaHang { get; set; } = default!;
        public string IDDonViVanChuyen { get; set; } = default!;
        public int PhiVanChuyen { get; set; }
        public string ThoiGianDuKien { get; set; } = default!;
        public DateTime NgayCapNhat { get; set; }

        // Thuộc tính từ DonViVanChuyen
        public string TenDonVi { get; set; } = default!;
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? MoTa { get; set; }
        public string TrangThai { get; set; } = default!;
    }
}
