using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shipping.Model
{
    public class SanPham
    {
        [Key]
        public string IDSanPham { get; set; } = default!;

        public int IDDanhMuc { get; set; }

        [ForeignKey("IDDanhMuc")]
        public DanhMuc DanhMuc { get; set; } = default!;

        public string IDCuaHang { get; set; }
        public string TenSanPham { get; set; }
        public byte[] HinhAnhChinh { get; set; }
        public string? MoTa { get; set; }
        public string? TrangThai { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public ICollection<BienTheSanPham> BienThes { get; set; } = new List<BienTheSanPham>();
    }
}
