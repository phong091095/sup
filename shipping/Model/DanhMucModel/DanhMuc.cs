using System.ComponentModel.DataAnnotations;

namespace shipping.Model.DanhMucModel
{
    public class DanhMuc
    {
        [Key]
        public int IDDanhMuc { get; set; }

        public string TenDanhMuc { get; set; } = default!;
        public int CapDanhMuc { get; set; }
        public string? TrangThai { get; set; }
        public bool IsLeaf { get; set; }
        public ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
        public ICollection<DanhMucChild> DanhMucChilds { get; set; } = new List<DanhMucChild>();
        public ICollection<DanhMucImages> DanhMucImages { get; set; } = new List<DanhMucImages>();
    }
}
