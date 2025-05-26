using Microsoft.AspNetCore.Mvc;
using shipping.Model;
using shipping.Model.DTO;
using shipping.Services.Implement;
using shipping.Services.Interface;
using System.Runtime.Intrinsics.Arm;

namespace shipping.Controllers
{
    [ApiController]
    [Route("store/products")]

    public class SanPhamController : Controller
    {
        public SanPhamSvc spsvc { get; set; }
        public BienTheSvc bienTheSvc { get; set; }
        public ImageSvc imageSvc { get; set; }
        public ShipSvc svc { get; set; }

        public SanPhamController(SanPhamSvc _spsvc, BienTheSvc bienTheSvc, ImageSvc imageSvc, ShipSvc svc)
        {
            spsvc = _spsvc;
            this.bienTheSvc = bienTheSvc;
            this.imageSvc = imageSvc;
            this.svc = svc;
        }
        //1
        //[HttpPost("request")]
        //public async Task<IActionResult> GetDataWithBodyRQ([FromBody] RequestbodyDTO request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest(new { thongBao = "Thông tin tìm kiếm không đủ" });
        //    }
        //    var res = await spsvc.GetDatabyRQ(request);
        //    if (res == null) {
        //        return BadRequest(new { thongBao = "Không tìm thấy dữ liệu" });
        //    }
        //    return Ok(res);
        //}
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSP(SanPhamDTO sp, [FromRoute] string id)
        {
            if (sp == null)
            {
                return BadRequest(new { thongBao = "Dữ liệu không đủ" });
            }
            var res = await spsvc.PutData(sp,id);
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
        [HttpPut("{id}/variants/{variantsId}")]
        public async Task<IActionResult> UpdateChiTietBT([FromBody] BienTheSPDTO dto, [FromRoute] string variantsId)
        {
            if (dto == null)
            {
                return BadRequest(new { thongBao = "Dữ liệu đầu vào không đầy đủ" });
            }
            var res = await spsvc.PutBienTheByID(dto, variantsId);
            if (!res)
            {
                return BadRequest(new { thongBao = "Vui lòng kiểm trả mã sản phẩm hoặc ID biến thể không tồn tại" });
            }
            return Ok("Cập nhật thành công");
        }
        //4.
        [HttpPut("shipping/{id}")]
        public async Task<IActionResult> UpdateCTVC([FromBody] ChiTietDVVanChuyenDTO data, [FromRoute] Guid id)
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
        [HttpPatch("{id}/shipping")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("ID không hợp lệ.");
            var res = await spsvc.UpdateStatus(id);
            if(res == 404)
            {
                return BadRequest("Không tồn tại chi tiết vận chuyển");
            }
            if(res == 200)
            {
                return Ok("Cập nhật trạng thái chi tiết thành công");
            }
            else
            {
                return BadRequest("Lỗi trong quá trình cập nhật. Vui lòng thử lại sau.");
            }
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
        [HttpPost("{id}/images")]
        public async Task<IActionResult> AddImage([FromRoute] string id, [FromBody] List<byte[]> images)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Mã sản phẩm trống");
            }
            if (!images.Any())
            {
                return BadRequest(new { thongBao = "Hình ảnh trống" });
            }
            var res = await imageSvc.AddImageByID(id, images);
            if (!res)
            {
                return BadRequest(new { thongBao = "Vui lòng kiểm tra lại mã sản phẩm. Số lượng ảnh không vượt quá 9." });
            }
            return Ok("Lưu thành công");
        }
        //6.1
        [HttpDelete("images")]
        public async Task<IActionResult> DeleteImage( [FromBody] List<int> idImage)
        {
            if (!idImage.Any())
            {
                return BadRequest("Không có mã ảnh.");
            }
            var res = await imageSvc.DeleteImageByID(idImage);
            if (res)
            {
                return Ok("Xóa ảnh thành công");
            }
            else
            {
                return BadRequest("Lỗi trong lúc xóa ảnh.");
            }
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
        [HttpPost("{id}/variants")]
        public async Task<IActionResult> CreateBTSP([FromBody]BienTheSPDTO data,[FromRoute]string id)
        {
            if (data == null)
            {
                return BadRequest("Dữ liệu không đầy đủ");
            }
            var res = await bienTheSvc.CreateData(data,id);
            if (res.Success)
            {
                return Ok(res.Message);
            }
            else
            {
                return BadRequest(res.Message);
            }
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
        public async Task<IActionResult> UpdateReview( [FromRoute] string id)
        {
            var res = await spsvc.PutReview(id);
            if (!res)
            {
                return BadRequest(new { thongBao = "Không thể gửi kiểm duyệt. Sản phẩm không tồn tại hoặc không ở trạng thái vi phạm" });
            }
            return Ok(new { thongBao = "Sản phẩm đã được cập nhật và chuyển sang trạng thái 'Đang hoạt động'." });
        }
        //11.
        [HttpPut("variants/{id}")]
        public async Task<IActionResult> UpdateGTBT([FromBody] GiaTriBienTheSanPhamDto data, [FromRoute] int id)
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
