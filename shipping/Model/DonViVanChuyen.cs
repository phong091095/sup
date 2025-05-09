using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class DonViVanChuyen
    {
        [Key]
        public string IDDonViVanChuyen { get; set; } = default!;
        [Required(ErrorMessage = "Vui lòng cung cấp tên đơn vị vân chuyển.")]
        [Length(3,255,ErrorMessage ="Tên đơn vị vân chuyển phải có độ dài từ 3 dến 255 ký tự.")]
        public string TenDonVi { get; set; }= default!;
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? SoDienThoai { get; set; } = default!;
        [EmailAddress(ErrorMessage ="Địa chỉ email không hợp lệ")]
        public string? Email { get; set; } = default!;
        public string? MoTa { get; set; } = default!;
        [EnumDataType(typeof(TrangThai), ErrorMessage = "Trạng thái không hợp lệ. Vui lòng chọn 'HoatDong' hoặc 'NgungHoatDong'")]
        public string TrangThai { get; set; } = default!;

    }


}
