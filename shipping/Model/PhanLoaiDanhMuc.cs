using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class PhanLoaiDanhMuc
    {
        [Key]
        public int Id { get; set; }
        public int IDDanhMucParent { get; set; }
        public string? Name { get; set; }
    }
}
