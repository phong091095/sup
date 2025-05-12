using Microsoft.AspNetCore.Mvc;
using shipping.Model;
using shipping.Model.DTO;
using shipping.Services.Implement;
using shipping.Services.Interface;

namespace shipping.Controllers
{
    [ApiController]
    [Route("store/products")]

    public class SanPhamController : Controller
    {
        public SanPhamSvc spsvc { get; set; }
        public ShipSvc shipsvc { get; set; }
        public BienTheSvc bienTheSvc { get; set; }
        public SanPhamController(SanPhamSvc _spsvc, ShipSvc shipsvc, BienTheSvc bienTheSvc)
        {
            spsvc = _spsvc;
            this.shipsvc = shipsvc;
            this.bienTheSvc = bienTheSvc;
        }
        //1
        [HttpPost("request")]
        public async Task<IActionResult> GetDataWithBodyRQ([FromBody] RequestbodyDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { thongBao = "Thông tin tìm kiếm không đủ" });
            }
            var res = await spsvc.GetDatabyRQ(request);
            if (res == null) {
                return BadRequest(new { thongBao = "Không tìm thấy dữ liệu" });
            }
            return Ok(res);
        }
        //1.1
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var res = await spsvc.GetDatas();
            if (res == null)
            {
                return BadRequest(new { thongBao = "Không tìm thấy dữ liệu" });
            }
            return Ok(res);
        }
        //2
        [HttpPut()]
        public async Task<IActionResult> UpdateSP(SanPhamDTO sp)
        {
            if (sp == null)
            {
                return BadRequest(new { thongBao = "Dữ liệu không đủ" });
            }
            var res = await spsvc.PutData(sp);
            if (res)
            {
                return Ok("Cập nhật thành công");
            }
            else
            {
                return BadRequest("Cập nhật thất bại");
            }
        }
        //3
        [HttpPut("variants")]
        public async Task<IActionResult> UpdateChiTietBT([FromBody] BienTheSPDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(new { thongBao = "Dữ liệu đầu vào không đầy đủ" });
            }
            var res = await spsvc.PutBienTheByID(dto);
            if (!res)
            {
                return BadRequest(new { thongBao = "Vui lòng kiểm trả mã sản phẩm hoặc ID biến thể không tồn tại" });
            }
            return Ok("Cập nhật thành công");
        }
        //4.
        [HttpPut("shipping")]
        public async Task<IActionResult> UpdateCTVC([FromBody] ChiTietDVVanChuyenDTO data)
        {
            if (data == null)
            {
                return BadRequest(new { thongBao = "Thông tin chi tiết đơn vị vận chuyển không đủ" });
            }
            var res = await shipsvc.PutData(data);
            if (!res)
            {
                return BadRequest(new { thongBao = "Không tìm thấy mã đỡ vị vận chuyển" });
            }
            return Ok("Cập nhật thành công");
        }
        //5.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSP([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Mã sản phẩm trống");
            }
            var res = await spsvc.DeleteData(id);
            if (!res)
            {
                return BadRequest(new { thongBao = "Lỗi trong quá trình xóa, vui lòng kiểm tra khóa." });
            }
            return Ok("Xóa thành công");
        }
        //6.
        [HttpPost("{id}")]
        public async Task<IActionResult> AddImage([FromRoute] string id, [FromBody] byte[] image)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Mã sản phẩm trống");
            }
            if (image == null)
            {
                return BadRequest(new { thongBao = "Hình ảnh trống" });
            }
            var res = await spsvc.AddImageByID(id, image);
            if (!res)
            {
                return BadRequest(new { thongBao = "Vui lòng kiểm tra lại mã sản phẩm." });
            }
            return Ok("Lưuthành công");
        }
        
        //7.
        [HttpGet("{id}/variant")]
        public async Task<IActionResult> GetBTSP([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Dữ liệu không đầy đủ");
            }
            var res = await bienTheSvc.GetDatasById(id);
            if (res == null)
            {
                return BadRequest(new { thongBao = "Không tìm thấy thông tin biến thể" });
            }
            return Ok(res);
        }
        //8.
        [HttpPost("variants")]
        public async Task<IActionResult> CreateBTSP([FromBody]BienTheSPDTO data)
        {
            if (data == null)
            {
                return BadRequest("Dữ liệu không đầy đủ");
            }
            var res = await bienTheSvc.CreateData(data);
            if (res == null)
            {
                return BadRequest(new { thongBao = "Thêm biến thể thất bại" });
            }
            return Ok(res);
        }
        //9.
        [HttpDelete("variants/{id}")]
        public async Task<IActionResult> DeleteBTSP([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Dữ liệu không đầy đủ");
            }
            var res = await bienTheSvc.DeleteData(id);
            if (!res)
            {
                return BadRequest(new { thongBao = "Vui lòng kiểm tra lại mã biến thể. Lỗi trong quá trình xóa" });
            }
            return Ok("Xóa thành công");
        }
        //10.
        [HttpPost("{id}/request-review")]
        public async Task<IActionResult> UpdateReview([FromBody] ProductDetail data)
        {
            if(data == null)
            {
                return BadRequest("Dữ liệu không đầy đủ");
            }
            var res = await spsvc.PutReview(data);
            if (!res)
            {
                return BadRequest(new { thongBao = "Không thể gửi kiểm duyệt. Sản phẩm không tồn tại hoặc không ở trạng thái vi phạm" });
            }
            return Ok(new { thongBao = "Sản phẩm đã được cập nhật và chuyển sang trạng thái 'Đang hoạt động'." });
        }
        //11.
        [HttpPut("variants/{id}")]
        public async Task<IActionResult> UpdateGTBT([FromBody] GiaTriBienTheSanPhamDto data, int id)
        {
            if(id == 0)
            {
                return BadRequest("Mã giá trị không tồn tại");
            }
            if(data == null)
            {
                return BadRequest("Thông tin không đầy đủ");
            }
            var res = await bienTheSvc.PutGT(data,id);
            if (!res)
            {
                return BadRequest("Vui lòng kiểm tra lại mã giá trị");
            }
            return Ok("Cập nhật thành công");
        }
    }
}
