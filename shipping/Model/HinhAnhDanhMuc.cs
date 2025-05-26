using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CategoriesService.Models
{
    public class HinhAnhDanhMuc
    {
        [Key]
        public int IDHinhAnhDanhMuc { get; set; }

        [ForeignKey("DanhMuc")]
        public int IDDanhMuc { get; set; }

        public byte[] HinhAnh { get; set; }

        // Tham chiếu đến danh mục
        public virtual DanhMuc DanhMuc { get; set; }
    }
}
