using Microsoft.AspNetCore.Mvc;
using shipping.Model;
using shipping.Services.Interface;

namespace shipping.Controllers
{
    [ApiController]
    [Route("admin/shipping-providers")]
    public class ShippingController : Controller
    {
        private readonly IShipServices<DonViVanChuyen> shipServices;
        private readonly IShipDTO<ChiTietDonViVanChuyenDTO> shipDTO;
        private readonly IPostDTO<DonViVanChuyen> postDTO;
        private readonly IPostDTO<ChiTietDVVanChuyen> postDetail;
        public ShippingController(IShipServices<DonViVanChuyen> shipServices, IShipDTO<ChiTietDonViVanChuyenDTO> shipDTO, IPostDTO<DonViVanChuyen> postDTO,IPostDTO<ChiTietDVVanChuyen> postDetail)
        {
            this.shipServices = shipServices;
            this.shipDTO = shipDTO;
            this.postDTO = postDTO;
            this.postDetail = postDetail;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDVVC(DonViVanChuyen dvvc)
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
            var res = await postDTO.CreateData(dvvc);
            return Ok(new
            {
                thongBao = "Thêm mới đơn vị vận chuyển thành công.",
                res
            });
        }
        [HttpPost("createdetail")]
        public async Task<IActionResult> CreateDetail(ChiTietDVVanChuyen detail)
        {
            if (detail == null)
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
            var res = await postDetail.CreateData(detail);
            if (res != null)
            {
                return Ok(new
                {
                    thongBao = "Thêm mới chi tiết đơn vị vận chuyển thành công.",
                    res
                });
            }
            else
            {
                return BadRequest(new
                {
                    thongBao = "Thêm mới chi tiết đơn vị vận chuyển thất bại.",
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDVVC(DonViVanChuyen dvvc)
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
            var res = await shipServices.UpdateData(dvvc);
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
            var res = await shipServices.UpdatePatchData(id);
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
            var res = await shipServices.GetDatas();
            if (res == null)
            {
                return BadRequest(new { thongBao = "Không lấy được dữ liệu vui lòng kiểm tra lại kết nối" });
            }
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDVByID([FromRoute] string id)
        {
            var res = await shipDTO.GetDataByIds(id);
            if (res == null)
            {
                return BadRequest(new { thongBao = "Vui lòng kiểm tra lại ID" });
            }
            return Ok(res);
        }
    }
}
