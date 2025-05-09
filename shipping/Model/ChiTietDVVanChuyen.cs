using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shipping.Model
{
    public class ChiTietDVVanChuyen
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();

        public string IDCuaHang { get; set; } = default!;

        [Required]
        public string IDDonViVanChuyen { get; set; } = default!;

        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phí vân chuyển phải là một số dương")]
        public int PhiVanChuyen { get; set; }

        public string ThoiGianDuKien { get; set; } = default!;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
    }
}
