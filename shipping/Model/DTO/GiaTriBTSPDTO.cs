using System.ComponentModel.DataAnnotations.Schema;

namespace shipping.Model.DTO
{
    public class GiaTriBTSPDTO
    {
        public int ID { get; set; }
        public int IDThuocTinh { get; set; }
        public string? TenGiaTri { get; set; } = default!;
        
    }
}
