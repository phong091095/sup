using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public byte[] HinhAnh { get; set; }
        public string IDSanPham {  get; set; }
    }
}
