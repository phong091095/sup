using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shipping.Services.Implement;

namespace shipping.Controllers
{
    [ApiController]
    [Route("admin/store")]

    public class CuaHangController : Controller
    {
        private readonly StoreSvc storeSvc;
        public CuaHangController(StoreSvc storeSvc) {
            this.storeSvc = storeSvc;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStore()
        {
            var res = await storeSvc.GetAll();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreByID([FromRoute] string id)
        {
            var res = await storeSvc.GetByID(id);
            if (string.IsNullOrEmpty(res.ID))
            {
                return BadRequest("Không tìm thấy cửa hàng. Vui lòng kiểm tra lại mã.");
            }
            return Ok(res);
        }
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> StoreApprove([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Thiếu mã cửa hàng");
            }
            var res = await storeSvc.PutApprove(id);
            if(res == 200)
            {
                return Ok("Cửa hàng đã được duyệt thành công");
            }
            else if (res == 404)
            {
                return NotFound("Không tìm thấy thông tin cửa hàng");
            }
            return BadRequest("Không thể duyệt cửa hàng này");
        }
        public class LyDoRequest
        {
            public string LyDo { get; set; } = string.Empty;
        }
        //[HttpPut("{id}/reject")]
        //public async Task<IActionResult> StoreReject([FromRoute] string id)
        //{

        //}
    }
}
