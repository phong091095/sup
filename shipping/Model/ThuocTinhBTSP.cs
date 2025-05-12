using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class ThuocTinhBTSP
    {
        [Key]
        public int ID { get; set; }
        public string TenThuocTinh { get; set; } = default!;
        public ICollection<GiaTriBTSP> GiaTris { get; set; } = new List<GiaTriBTSP>();
    }
}
