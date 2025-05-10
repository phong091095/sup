using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class ChiTietBienTheSanPham
    {
        [Key]
        public int IDChiTietBTSanPham { get; set; }
        public string IDBienTheSanPham { get; set; }
        public int IDGiaTriBienTheSanPham { get; set; }
        public string MoTa { get; set; } = default!;
    }
}
