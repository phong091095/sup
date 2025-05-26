using CategoriesService.Models;

namespace CategoriesService.DTO
{
    public class DanhMucDTO
    {
        public int IdDanhMuc { get; set; }
        public string TenDanhMuc { get; set; } = string.Empty;
        public int CapDanhMuc { get; set; }
        public string Path { get; set; } = string.Empty;
        public bool TrangThai { get; set; }
        public bool IsLeaf { get; set; }
        public int SoLuongSanPhamLienQuan { get; set; }

        public List<HinhAnhDanhMucDTO> HinhAnhs { get; set; } = new();
        public List<DanhMucDTO> Children { get; set; } = new();
    }

    public class HinhAnhDanhMucDTO
    {
        public int IdHinhAnhDanhMuc { get; set; }
        public byte[] HinhAnh { get; set; }
    }
    public class MoveDanhMucDTO
    {
        public int IdDanhMuc { get; set; }
        public int CapDanhMuc { get; set; }
    }

}
