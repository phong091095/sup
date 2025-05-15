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
        //1.1
        [HttpGet]
        public async Task<IActionResult> GetAllStore()
        {
            var res = await storeSvc.GetAll();
            return Ok(res);
        }
        //1.2
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreByID([FromRoute] string id)
        {
            var res = await storeSvc.GetByID(id);
            if (string.IsNullOrEmpty(res.IDCuaHang))
            {
                return BadRequest("Không tìm thấy cửa hàng. Vui lòng kiểm tra lại mã.");
            }
            return Ok(res);
        }

        //1.3
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
        //1.4
        public class LyDoRequest
        {
            public string LyDo { get; set; } = string.Empty;
        }
        [HttpPut("{id}/reject")]
        public async Task<IActionResult> StoreReject([FromRoute] string id, [FromBody] LyDoRequest lydo)
        {
            if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(lydo.LyDo)){
                return BadRequest("Vui lòng kiểm tra ID hoặc nội dung lý do từ chối");
            }
            int res = await storeSvc.RejectStore(id,lydo.LyDo);
            if(res == 200)
            {
                return Ok("Đã từ chôi đăng ký cửa hàng.");
            }
            else if(res == 404)
            {
                return NotFound("Vui lòng kiểm tra lại mã cửa hàng hoặc thông tin người dùng.");
            }
            else if(res == 400)
            {
                return BadRequest("Lỗi trong quá trình gửi email từ chối");
            }
            else
            {
                return BadRequest("Không thể từ chối đăng ký cửa hàng này. Vui lòng kiểm tra trạng thái");
            }
        }
        //
        [HttpPost("{id}/request-more-info")]
        public async  Task<IActionResult> GetStatusInfo([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Vui lòng kiểm tra lại mã cửa hàng");
            }
            var res = await storeSvc.GetStoreStatusByID(id);
            if(res == "Không tìm thấy thông tin cửa hàng. Vui lòng kiểm tra lại mã.")
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        //1.5
        [HttpPost("{id}/unlock")]
        public async Task<IActionResult> PutStatusInfo([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Vui lòng kiểm tra lại mã cửa hàng");
            }
            var res = await storeSvc.PutStoreStatus(id);
            if(res == 404)
            {
                return BadRequest("Không tồn tại mã cửa hàng");
            }
            return Ok("Đã mở khóa cửa hàng");
        }
        //1.6
        [HttpPost("{id}/lock")]
        public async Task<IActionResult> PutLockStore([FromRoute] string id, [FromBody] LyDoRequest lydo)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Vui lòng kiểm tra lại mã cửa hàng");
            }
            var res = await storeSvc.LockStore(id,lydo.LyDo);
            if (!res)
            {
                return BadRequest("Vui lòng kiểm tra lại mã cửa hàng. Hoặc lỗi trong quá trình gửi email.");
            }
            return Ok("Đã khóa cửa hàng");
        }
        //1.7
        [HttpGet("{id}/activities")]
        public async Task<IActionResult> IGetActive()
        {
            var res = await storeSvc.GetAllActive();
            return Ok(res);
        }
        public class MailInfo
        {
            public string tieuDe { get; set; } = default!;
            public string noiDung { get; set; } = default!;
        }
        //1.8
        [HttpPost("{id}/notify")]
        public async Task<IActionResult> SendNotify([FromRoute] string id, [FromBody] MailInfo info)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return BadRequest("Mã cửa hàng không có.");
            }
            var res = await storeSvc.SendNotify(id, info.tieuDe, info.noiDung);
            if (!res) {
                return BadRequest("Vui lòng kiểm tra lại mã cửa hàng. Hoặc lỗi trong quá trình gửi email.");
            }
            return Ok("Đã gửi thông báo đến cửa hàng");
        }
    }
}
