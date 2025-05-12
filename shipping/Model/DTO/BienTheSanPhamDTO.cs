namespace shipping.Model.DTO
{
    public class BienTheSPDTO
    {
        public string IDBienTheSanPham { get; set; } = default!;
        public decimal Gia { get; set; } = 0;
        public int SoLuong { get; set; } = 0;
        public string? SKU { get; set; }
        public byte[]? HinhAnhBienThe { get; set; }
        public string IDSanPham { get; set; } = default!;
    }
}
