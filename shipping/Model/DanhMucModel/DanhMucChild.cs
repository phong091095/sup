using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shipping.Model.DanhMucModel
{
    public class DanhMucChild
    {
        [Key]
        public int Id { get; set; }
        public string TenDanhMuc { get; set; }
        public int IDDanhMuc { get; set; }
        public byte[] Images { get; set; }
        [ForeignKey("IDDanhMuc")]
        public DanhMuc DanhMuc { set; get; }

    }
}
