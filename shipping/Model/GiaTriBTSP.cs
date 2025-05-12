using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shipping.Model
{
    public class GiaTriBTSP
    {
        [Key]
        public int ID { get; set; }
        public int IDThuocTinh { get; set; }
        public string TenGiaTri { get; set; } = default!;
        [ForeignKey("IDThuocTinh")]
        public ThuocTinhBTSP ThuocTinh { get; set; } = default!;

        public ICollection<ChiTietBienTheSanPham> ChiTietBienThes { get; set; } = new List<ChiTietBienTheSanPham>();
    }
}
