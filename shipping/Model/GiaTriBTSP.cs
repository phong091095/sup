using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class GiaTriBTSP
    {
        [Key]
        public int ID { get; set; }
        public int IDThuocTinh { get; set; }
        public string TenGiaTri { get; set; } = default!;
    }
}
