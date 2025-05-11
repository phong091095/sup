using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;
using static shipping.Model.TrangThaiTong;

namespace shipping.Services.Implement
{
    public class SanPhamSvc : IGetDTO<ProductDetail>,
        IAddImage,IGetByRQ<ProductDetail>,IDeleTeDTO<SanPham>,IPutData<ProductDetail>,IPutSp<SanPham>
    {
        private readonly Context _context;
        public SanPhamSvc(Context context)
        {
            _context = context;
        }

        public async Task<bool> AddImageByID(string id, byte[] imgage)
        {
            var exists = await _context.SanPham.FirstOrDefaultAsync(x => x.IDSanPham == id);
            if (exists == null)
            {
                return false;
            }
            exists.HinhAnhChinh = imgage;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteData(string id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var exists = await _context.SanPham.FirstOrDefaultAsync(x => x.IDSanPham == id);
                if (exists == null)
                    return false;

                var bt = await _context.BienTheSanPham.FirstOrDefaultAsync();
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
                var bienTheList = bienThes.Where(bt => bt.IDSanPham == sp.IDSanPham).ToList();

                var product = new ProductDetail
                {
                    SanPham = sp,
                    DanhMuc = danhMucs.FirstOrDefault(d => d.IDDanhMuc == sp.IDDanhMuc),
                    PhanLoai = phanLoais.FirstOrDefault(p => p.Id == sp.IDPhanLoaiDanhMuc),
                    BienTheSanPham = bienTheList.Select(bienThe => new BienTheSanPhamDTO
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
                    }).ToList()
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
                var bienTheList = bienThes.Where(bt => bt.IDSanPham == sp.IDSanPham).ToList();

                var product = new ProductDetail
                {
                    SanPham = sp,
                    DanhMuc = danhMucs.FirstOrDefault(d => d.IDDanhMuc == sp.IDDanhMuc),
                    PhanLoai = phanLoais.FirstOrDefault(p => p.Id == sp.IDPhanLoaiDanhMuc),
                    BienTheSanPham = bienTheList.Select(bienThe => new BienTheSanPhamDTO
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
                    }).ToList()
                };

                products.Add(product);
            }

            return Task.FromResult(products);
        }

        public async Task<bool> PutBienTheByID(BienTheSanPham type)
        {
            var bt = await _context.BienTheSanPham.FirstOrDefaultAsync(x=>x.IDBienTheSanPham == type.IDBienTheSanPham);
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

        public async Task<bool> PutData(ProductDetail type)
        {
            var exists = await _context.SanPham.FirstOrDefaultAsync(x => x.IDSanPham == type.SanPham.IDSanPham);
            if (exists == null)
            {
                return false;
            }
            var bt = await _context.BienTheSanPham.Where(x => x.IDSanPham == exists.IDSanPham).ToListAsync();
            if (!bt.Any())
            {
                return false;
            }
            exists.MoTa = type.SanPham.MoTa;
            exists.TenSanPham = type.SanPham.TenSanPham;
            exists.HinhAnhChinh = type.SanPham.HinhAnhChinh;
            exists.IDDanhMuc = type.SanPham.IDDanhMuc;
            var listdto = type.BienTheSanPham;
            foreach (var item in listdto) {
                var bienThe = item.bienthe;
                var thongtin = item.GiaTriBienTheSanPham;
                var existsbt = bt.FirstOrDefault(x => x.IDBienTheSanPham == bienThe.IDBienTheSanPham);
                if (existsbt == null) {
                    continue;
                }
                existsbt.SKU = bienThe.SKU;
                existsbt.Gia = bienThe.Gia;
                existsbt.HinhAnhBienThe = bienThe.HinhAnhBienThe;
                existsbt.SoLuong = bienThe.SoLuong;
                foreach(var ttitem in thongtin)
                {
                    var gt = _context.GiaTriBTSP.FirstOrDefault(x => x.TenGiaTri == ttitem.TenGiaTri);
                    if (gt == null) continue;

                    var tt = _context.ThuocTinhBTSP.FirstOrDefault(x => x.ID == gt.IDThuocTinh);
                    if (tt == null) continue;
                    tt.TenThuocTinh = ttitem.TenThuocTinh;
                    gt.TenGiaTri = ttitem.TenGiaTri;
                }
            }
            if (exists.TrangThai == TrangThaiTong.TrangThaiSP.ViPham.ToString())
            {
                exists.TrangThai = TrangThaiTong.TrangThaiSP.DangHoatDong.ToString();
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutData(SanPham type)
        {
            var exists = await _context.SanPham.FirstOrDefaultAsync(x=>x.IDSanPham == type.IDSanPham);
            if (exists == null)
            {
                return false;
            }
            exists.IDDanhMuc = type.IDDanhMuc;
            exists.MoTa = type.MoTa;
            exists.TenSanPham = type.TenSanPham;    
            exists.HinhAnhChinh = type.HinhAnhChinh;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
