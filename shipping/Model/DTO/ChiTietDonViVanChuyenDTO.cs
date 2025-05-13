using System.ComponentModel.DataAnnotations;

namespace shipping.Model.DTO
{
    public class ChiTietDonViVanChuyenDTO
    {
        //Danh sách Chi tiết
        public List<ChiTietDVVanChuyenDTO> dsdetail { get; set; }   = new List<ChiTietDVVanChuyenDTO> { };
        // Thuộc tính từ DonViVanChuyen
        public DonViVanChuyen dvvc { get; set; } = new DonViVanChuyen();
    }
   
}
