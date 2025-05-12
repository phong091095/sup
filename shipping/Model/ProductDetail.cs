namespace shipping.Model
{
    public class ProductDetail
    {
        public SanPham SanPham { get; set; }
        public string DanhMuc { get; set; }
        public List<BienTheSanPhamDTO> BienTheSanPham { get; set; } = new();
    }

    public class BienTheSanPhamDTO
    {
        public BienTheSanPham bienthe { get; set; }
        public List<GiaTriBienTheSanPhamDto> GiaTriBienTheSanPham { get; set; } = new();
    }

    public class GiaTriBienTheSanPhamDto
    {
        public string TenThuocTinh { get; set; }
        public string TenGiaTri { get; set; }
    }
    public class RequestbodyDTO
    {
        public string? TenSanPham { get; set;}
        public string? Path { get; set;}
    }
}
