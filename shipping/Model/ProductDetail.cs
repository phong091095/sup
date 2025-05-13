using shipping.Model.DTO;

namespace shipping.Model
{
    public class ProductDetail
    {
        public SanPhamDTO SanPham { get; set; }
        public List<byte[]> Images { get; set; } = new();
        public string DanhMuc { get; set; }
        public List<BienTheSanPhamDTO> BienTheSanPham { get; set; } = new();
    }

    public class BienTheSanPhamDTO
    {
        public BienTheSPDTO bienthe { get; set; }
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
    public class ProductReview
    {
        public string IDBienTheSanPham { get; set; } = default!;
        public string PathDanhMuc { get; set; } = default;
        public byte[] HinhAnh { get; set; } = default!;
        public string TenSanPham {get;set;} = default!;
        public string MoTa { get; set; } = default!;
        public List<BienTheSanPhamDTO> BienTheSanPham { get; set; } = new();
    }
}
