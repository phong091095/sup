using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shipping.Model
{
    public class ChiTietBienTheSanPham
    {
        [Key]
        public int IDChiTietBTSanPham { get; set; }

        public string IDBienTheSanPham { get; set; } = default!;
        public int IDGiaTriBienTheSanPham { get; set; }
        public string MoTa { get; set; } = default!;

        [ForeignKey("IDBienTheSanPham")]
        public BienTheSanPham BienTheSanPham { get; set; } = default!;
        [ForeignKey("IDGiaTriBienTheSanPham")]
        public GiaTriBTSP GiaTri { get; set; } = default!;
    }
}
