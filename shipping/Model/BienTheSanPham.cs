using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shipping.Model
{
    public class BienTheSanPham
    {
        [Key]
        public string IDBienTheSanPham { get; set; } = default!;
        [Precision(18, 2)]
        public decimal Gia { get; set; } = 0;
        public int SoLuong { get; set; } = 0;
        public string? SKU { get; set; }
        public byte[]? HinhAnhBienThe {  get; set; }
        public string IDSanPham { get; set; } = default!;
        [ForeignKey("IDSanPham")]
        public SanPham SanPham { get; set; } = default!;
        public ICollection<ChiTietBienTheSanPham> ChiTietBienThes { get; set; } = new List<ChiTietBienTheSanPham>();
    }
}
