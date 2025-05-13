using Microsoft.AspNetCore.Mvc;
using shipping.Model;
using shipping.Model.DTO;
using shipping.Services.Implement;
using shipping.Services.Interface;

namespace shipping.Controllers
{
    [ApiController]
    [Route("admin/shipping-providers")]
    public class ShippingController : Controller
    {
        
        public ShipSvc svc { get; set; }
        public ShippingController(ShipSvc svc)
        {
            this.svc = svc;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDVVC([FromBody] DonViVanChuyenDTO dvvc)
        {
            if (dvvc == null)
            {
                return BadRequest("Dữ liệu truyền vào bị thiếu");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new
                {
                    thongBao = errors
                });
            }
            var res = await svc.CreateDVVC(dvvc);
            return Ok(new
            {
                thongBao = "Thêm mới đơn vị vận chuyển thành công.",
                res
            });
        }
        [HttpPost("detail")]
        public async Task<IActionResult> CreateDetailVC([FromBody] List<ChiTietDVVanChuyenDTO> details)
        {
            if (!details.Any())
            {
                return BadRequest("Dữ liệu truyền vào bị thiếu");

            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new
                {
                    thongBao = errors
                });
            }
            var res = await svc.CreateCTVC(details);
            return Ok("Tạo chi tiết thành công");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDVVC(DonViVanChuyenDTO dvvc, [FromRoute] string id)
        {
            if (dvvc == null)
            {
                return BadRequest("Dữ liệu truyền vào bị thiếu");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new
                {
                    thongBao = errors
                });
            }
            var res = await svc.UpdateData(dvvc,id);
            if (res == "Cập nhật thành công")
            {
                return Ok(new { thongBao = res });
            }
            else
            {
                return BadRequest(new { thongBao = res });
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchDVVC([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new { thongBao = "ID truyền vào bị trống" });
            }
            var res = await svc.UpdatePatchData(id);
            if (res)
            {
                return Ok(new { thongBao = "Cập nhật thành công" });
            }
            else
            {
                return BadRequest(new { thongBao = "Cập nhật trạng thái thất bại. Vui lòng kiểm tra lại ID." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var res = await svc.GetDatas();
            if (res == null)
            {
                return BadRequest(new { thongBao = "Không lấy được dữ liệu vui lòng kiểm tra lại kết nối" });
            }
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDVByID([FromRoute] string id)
        {
            var res = await svc.GetDataByIds(id);
            if (res == null)
            {
                return BadRequest(new { thongBao = "Vui lòng kiểm tra lại ID" });
            }
            return Ok(res);
        }
        //4.
        [HttpPut("shipping/{id}")]
        public async Task<IActionResult> UpdateCTVC([FromBody] ChiTietDVVanChuyenDTO data, Guid id)
        {
            if (data == null)
            {
                return BadRequest(new { thongBao = "Thông tin chi tiết đơn vị vận chuyển không đủ" });
            }
            var res = await svc.PutShipping(data, id);
            if (!res)
            {
                return BadRequest(new { thongBao = "Không tìm thấy mã đơn vị vận chuyển" });
            }
            return Ok("Cập nhật thành công");
        }
    }
}
