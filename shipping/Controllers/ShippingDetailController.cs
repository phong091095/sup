using Microsoft.AspNetCore.Mvc;
using shipping.Model;
using shipping.Services.Interface;

namespace shipping.Controllers
{
    [ApiController]
    [Route("admin/shipping-detail")]
    public class ShippingDetailController : Controller
    {
        private readonly IShipServices<ChiTietDVVanChuyen> shipServices;
        public ShippingDetailController(IShipServices<ChiTietDVVanChuyen> shipServices)
        {
            this.shipServices = shipServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDetail(ChiTietDVVanChuyen data)
        {
            if (data == null)
            {
                return BadRequest(new { thongBao = "Dữ liệu truyền vào bị thiếu" });
            }
            var res = shipServices.CreateData(data);
            if (res == null) {
                return BadRequest(new { thongBao = "Không tìm thấy mã đơn vị vận chuyển trùng khớp" });
            }
            return Ok(new {thongBao = "Tạo chi tiết thành công"});
        }
    }
}
