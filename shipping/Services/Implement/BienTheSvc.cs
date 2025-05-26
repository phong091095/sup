using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Model.DTO;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class BienTheSvc : IBienThe
    {
        private readonly Context _context;
        private readonly ImageSvc _imageSvc;
        public BienTheSvc(Context context, ImageSvc imageSvc)
        {
            _context = context;
            _imageSvc = imageSvc;
        }

        public async Task<BResponse> CreateData(BienTheSPDTO type, string id)
       {
            if(type.Gia < 1000)
            {
                return new BResponse( false, "Số tiền ít nhất là 1000 trở lên", 400);
            }
            if(type.SoLuong < 0)
            {
                return new BResponse(false, "Số lượng ít nhất là từ 0 trở lên", 400);
            }
            if(type.HinhAnhBienThe == null || type.HinhAnhBienThe.Length == 0)
            {
                return new BResponse(false, "Mhập đúng định dạng hình ảnh", 400);
            }
            var count = _context.BienTheSanPham.Count() + 1;
            var item = new BienTheSanPham
            {
                IDBienTheSanPham = "BTSP" + Guid.NewGuid().ToString("N").Substring(0, 8),
                IDSanPham = id,
                Gia = type.Gia,
                SKU = type.SKU,
                SoLuong = type.SoLuong,
                HinhAnh = type.HinhAnhBienThe
            };
            await _context.BienTheSanPham.AddAsync(item);
            return new BResponse(true, "Tạo mới biến thể thành công", 200);
        }
        public async Task<bool> DeleteData(string id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var bt = await _context.BienTheSanPham
                    .Include(x => x.ChiTietBienThes)
                    .FirstOrDefaultAsync(x => x.IDBienTheSanPham == id);

                if (bt == null)
                    return false;

                foreach (var item in bt.ChiTietBienThes)
                {
                    var gt = await _context.GiaTriBTSP.FirstOrDefaultAsync(x => x.ID == item.IDGiaTriBienTheSanPham);
                    if (gt != null)
                    {
                        bool isUsed = await _context.ChiTietBienTheSanPham
                            .AnyAsync(x => x.IDGiaTriBienTheSanPham == gt.ID && x.IDBienTheSanPham != bt.IDBienTheSanPham);

                        if (!isUsed)
                        {
                            var tt = await _context.ThuocTinhBTSP.FirstOrDefaultAsync(x => x.ID == gt.IDThuocTinh);

                            bool isTTUsed = await _context.GiaTriBTSP
                                .AnyAsync(g => g.IDThuocTinh == gt.IDThuocTinh && g.ID != gt.ID);

                            if (!isTTUsed && tt != null)
                                _context.ThuocTinhBTSP.Remove(tt);

                            _context.GiaTriBTSP.Remove(gt);
                        }
                    }
                }

                _context.ChiTietBienTheSanPham.RemoveRange(bt.ChiTietBienThes);
                _context.BienTheSanPham.Remove(bt);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }


        public async Task<List<BienTheSanPham>> GetDatasById(string id)
        {
            var res = await _context.BienTheSanPham.Where(x => x.IDSanPham == id).ToListAsync();
            if (res == null)
            {
                return new List<BienTheSanPham>();
            }
            return res;
        }

        public async Task<bool> PutGT(GiaTriBienTheSanPhamDto dto, int id)
        {
            var gt = await _context.GiaTriBTSP.FirstOrDefaultAsync(x => x.ID == id);
            if ( gt == null)
            {
                return false;
            }
            var tt = await _context.ThuocTinhBTSP.FirstOrDefaultAsync(x => x.ID == gt.IDThuocTinh);
            if(tt == null)
            {
                return false;
            }
            gt.TenGiaTri = dto.TenGiaTri;
            tt.TenThuocTinh = dto.TenThuocTinh;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
