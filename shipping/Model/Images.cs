using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shipping.Model
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public byte[] HinhAnh { get; set; }
        public string IDSanPham { get; set; }

        [ForeignKey("IDSanPham")]
        public SanPham SanPham { get; set; } = default!;
    }
}
