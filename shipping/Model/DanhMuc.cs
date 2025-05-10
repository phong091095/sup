using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class DanhMuc
    {
        [Key]
        public int IDDanhMuc { get; set; }
        public string? TenDanhMuc { get; set; }
        public string? TrangThai { get; set; }
        public bool IsLeaf { get; set; }
    }
}
