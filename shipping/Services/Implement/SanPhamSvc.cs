using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;
using static shipping.Model.TrangThaiTong;

namespace shipping.Services.Implement
{
    public class SanPhamSvc : IGetDTO<ProductDetail>,IGetByRQ<ProductDetail>,IPutByID<BienTheSanPham>,IDeleTeDTO<SanPham>,IPutData<ProductDetail>
    {
        private readonly Context _context;
        public SanPhamSvc(Context context)
        {
            _context = context;
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
                _context.SanPham.Remove(exists);

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


        public Task<List<ProductDetail>> GetDatabyRQ(RequestbodyDTO request)
        {
            if (request == null)
                return Task.FromResult<List<ProductDetail>>(null);

            var sanPhams = _context.SanPham.ToList();
            var danhMucs = _context.DanhMuc.ToList();
            var phanLoais = _context.PhanLoaiDanhMuc.ToList();
            var bienThes = _context.BienTheSanPham.ToList();
            var chiTietBienThes = _context.ChiTietBienTheSanPham.ToList();
            var thuocTinhs = _context.ThuocTinhBTSP.ToList();
            var giaTris = _context.GiaTriBTSP.ToList();

            var filteredSanPhams = sanPhams
                .Where(sp =>
                    (string.IsNullOrEmpty(request.TenSanPham) || sp.TenSanPham.Contains(request.TenSanPham, StringComparison.OrdinalIgnoreCase)) &&
                    (request.TenDanhMuc == null || request.TenDanhMuc.Count == 0 || request.TenDanhMuc.Contains(
                        danhMucs.FirstOrDefault(dm => dm.IDDanhMuc == sp.IDDanhMuc)?.TenDanhMuc ?? string.Empty)) &&
                    (request.TenPhanLoai == null || request.TenPhanLoai.Count == 0 || request.TenPhanLoai.Contains(
                        phanLoais.FirstOrDefault(pl => pl.Id == sp.IDPhanLoaiDanhMuc)?.Name ?? string.Empty))
                ).ToList();

            var products = new List<ProductDetail>();

            foreach (var sp in filteredSanPhams)
            {
                var bienThe = bienThes.FirstOrDefault(bt => bt.IDBienTheSanPham == sp.IDBienTheSanPham);

                var product = new ProductDetail
                {
                    SanPham = sp,
                    DanhMuc = danhMucs.FirstOrDefault(d => d.IDDanhMuc == sp.IDDanhMuc),
                    PhanLoai = phanLoais.FirstOrDefault(p => p.Id == sp.IDPhanLoaiDanhMuc),
                    BienTheSanPham = 
                    new BienTheSanPhamDTO
                    {
                        bienthe = bienThe,
                        GiaTriBienTheSanPham = chiTietBienThes
                            .Where(ct => ct.IDBienTheSanPham == bienThe.IDBienTheSanPham)
                            .Select(ct =>
                            {
                                var giaTri = giaTris.FirstOrDefault(g => g.ID == ct.IDGiaTriBienTheSanPham);
                                var thuocTinh = thuocTinhs.FirstOrDefault(t => t.ID == giaTri?.IDThuocTinh);

                                return new GiaTriBienTheSanPhamDto
                                {
                                    TenGiaTri = giaTri?.TenGiaTri,
                                    TenThuocTinh = thuocTinh?.TenThuocTinh
                                };
                            }).ToList()
                    }
                };

                products.Add(product);
            }

            return Task.FromResult(products);
        }



        public Task<List<ProductDetail>> GetDatas()
        {
            var sanPhams = _context.SanPham.ToList();
            var danhMucs = _context.DanhMuc.ToList();
            var phanLoais = _context.PhanLoaiDanhMuc.ToList();
            var bienThes = _context.BienTheSanPham.ToList();
            var chiTietBienThes = _context.ChiTietBienTheSanPham.ToList();
            var thuocTinhs = _context.ThuocTinhBTSP.ToList();
            var giaTris = _context.GiaTriBTSP.ToList();

            var products = new List<ProductDetail>();

            foreach (var sp in sanPhams)
            {
                var bienThe = bienThes.FirstOrDefault(bt => bt.IDBienTheSanPham == sp.IDBienTheSanPham);

                var product = new ProductDetail
                {
                    SanPham = sp,
                    DanhMuc = danhMucs.FirstOrDefault(d => d.IDDanhMuc == sp.IDDanhMuc),
                    PhanLoai = phanLoais.FirstOrDefault(p => p.Id == sp.IDPhanLoaiDanhMuc),
                    BienTheSanPham = 
                    new BienTheSanPhamDTO
                    {
                        bienthe = bienThe,
                        GiaTriBienTheSanPham = chiTietBienThes
                            .Where(ct => ct.IDBienTheSanPham == bienThe.IDBienTheSanPham)
                            .Select(ct =>
                            {
                                var giaTri = giaTris.FirstOrDefault(g => g.ID == ct.IDGiaTriBienTheSanPham);
                                var thuocTinh = thuocTinhs.FirstOrDefault(t => t.ID == giaTri?.IDThuocTinh);

                                return new GiaTriBienTheSanPhamDto
                                {
                                    TenGiaTri = giaTri?.TenGiaTri,
                                    TenThuocTinh = thuocTinh?.TenThuocTinh
                                };
                            }).ToList()
                    }
                };

                products.Add(product);
            }

            return Task.FromResult(products);
        }


        public async Task<bool> PutBienTheByID(string id,BienTheSanPham type)
        {
            var exists = await _context.SanPham.FirstOrDefaultAsync(x=>x.IDSanPham == id);
            if (exists == null) {

                return false;
            }
            var bt = await _context.BienTheSanPham.FirstOrDefaultAsync(x=>x.IDBienTheSanPham == exists.IDBienTheSanPham);
            if (bt == null) {
                return false;
            }
            bt.HinhAnhBienThe = type.HinhAnhBienThe;
            bt.Gia = type.Gia;
            bt.SKU = type.SKU;
            bt.SoLuong = type.SoLuong;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutChiTietBTByID(string id, GiaTriBienTheSanPhamDto type)
        {
            var exists = await _context.SanPham.FirstOrDefaultAsync(x => x.IDSanPham == id);
            if (exists == null)
            {

                return false;
            }
            var bt = await _context.BienTheSanPham.FirstOrDefaultAsync(x => x.IDBienTheSanPham == exists.IDBienTheSanPham);
            if (bt == null)
            {
                return false;
            }
            var ct = await _context.ChiTietBienTheSanPham.Where(x=>x.IDBienTheSanPham == bt.IDBienTheSanPham).ToListAsync();
            foreach (var item in ct) {
                var gt = await _context.GiaTriBTSP.FirstOrDefaultAsync(x => x.ID == item.IDGiaTriBienTheSanPham);
                if (gt == null)
                {
                    continue;
                }
                if (gt.TenGiaTri == type.TenGiaTri) {
                    var tt = await _context.ThuocTinhBTSP.FirstOrDefaultAsync(x => x.ID == gt.IDThuocTinh);
                    if (tt == null) { return false; }
                    tt.TenThuocTinh = type.TenThuocTinh;
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutData(ProductDetail type)
        {

            var exists = await _context.SanPham.FirstOrDefaultAsync(x => x.IDSanPham == type.SanPham.IDSanPham);
            if (exists == null)
            {
                return false;
            }
            var bt = await _context.BienTheSanPham.FirstOrDefaultAsync(x => x.IDBienTheSanPham == exists.IDBienTheSanPham);
            if (bt == null)
            {
                return false;
            }
            exists.MoTa = type.SanPham.MoTa;
            exists.TenSanPham = type.SanPham.TenSanPham;
            exists.HinhAnhChinh = type.SanPham.HinhAnhChinh;
            exists.IDDanhMuc = type.SanPham.IDDanhMuc;
            bt.HinhAnhBienThe = type.BienTheSanPham.bienthe.HinhAnhBienThe;
            bt.SKU = type.BienTheSanPham.bienthe.SKU;
            bt.Gia = type.BienTheSanPham.bienthe.Gia; ;
            bt.SoLuong = type.BienTheSanPham.bienthe.SoLuong;
            if (exists.TrangThai == TrangThaiTong.TrangThaiSP.ViPham.ToString())
            {
                exists.TrangThai = TrangThaiTong.TrangThaiSP.DangHoatDong.ToString();
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
