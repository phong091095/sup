using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class BienTheSvc : IGetDTO<BienTheSanPham>, IPostDTO<BienTheSanPham>, IDeleTeDTO<BienTheSanPham>
    {
        private readonly Context _context;
        public BienTheSvc(Context context)
        {
            _context = context;
        }

        public async Task<BienTheSanPham> CreateData(BienTheSanPham type)
        {
            var count = _context.BienTheSanPham.Count() + 1;
            type.IDBienTheSanPham = "BTSP" + count;
            await _context.BienTheSanPham.AddAsync(type);
            return type;
        }

        public async Task<bool> DeleteData(string id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var exists = await _context.SanPham.FirstOrDefaultAsync(x => x.IDSanPham == id);
                if (exists == null)
                    return false;

                var bt = await _context.BienTheSanPham.FirstOrDefaultAsync(x => x.IDBienTheSanPham == exists.IDBienTheSanPham);
                if (bt == null)
                    return false;

                var ctList = await _context.ChiTietBienTheSanPham
                    .Where(x => x.IDBienTheSanPham == bt.IDBienTheSanPham)
                    .ToListAsync();

                foreach (var item in ctList)
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

                _context.ChiTietBienTheSanPham.RemoveRange(ctList);
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

        public async Task<List<BienTheSanPham>>GetDatas()
        {
            var res = await _context.BienTheSanPham.ToListAsync();
            if (res == null)
            {
                return new List<BienTheSanPham>();
            }
            return res;
        }
    }
}
