using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Model.DTO;
using shipping.Services.Interface;
using static shipping.Model.TrangThaiTong;

namespace shipping.Services.Implement
{
    public class SanPhamSvc : IGetDTO<ProductDetail>,IGetByRQ<ProductDetail>,IDeleTeDTO<SanPham>,IPutReview<ProductReview>, IPutSp<SanPhamDTO>,IPutStatus
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

                var bt = await _context.BienTheSanPham.FirstOrDefaultAsync(x => x.IDSanPham == id);

                List<ChiTietBienTheSanPham> ctList = new();
                if (bt != null)
                {
                    ctList = await _context.ChiTietBienTheSanPham
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
                }

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



        public async Task<List<ProductDetail>?> GetDatabyRQ(RequestbodyDTO request)
        {
            var sanPhams = await _context.SanPham
                .Include(sp => sp.DanhMuc)
                .Include(sp => sp.Images)
                .Include(sp => sp.BienThes)
                    .ThenInclude(bt => bt.ChiTietBienThes)
                        .ThenInclude(ct => ct.GiaTri)
                            .ThenInclude(gt => gt.ThuocTinh)
                            .Where(sp =>
            (string.IsNullOrEmpty(request.TenSanPham) || sp.TenSanPham.ToLower().Contains(request.TenSanPham.ToLower())) &&
            (string.IsNullOrEmpty(request.Path) || sp.DanhMuc.Path.ToLower().Contains(request.Path.ToLower()))
        )
                                    .ToListAsync();

            if (sanPhams == null || !sanPhams.Any())
                return null;

            var products = sanPhams.Select(sp => new ProductDetail
            {
                SanPham = new SanPhamDTO
                {
                    PathDanhMuc = sp.DanhMuc.Path,
                    IDCuaHang = sp.IDCuaHang,
                    TenSanPham = sp.TenSanPham,
                    MoTa = sp.MoTa,
                    TrangThai = sp.TrangThai,
                    NgayTao = sp.NgayTao
                },
                Images = sp.Images.Select(x=>x.HinhAnh).ToList()
                ,
                DanhMuc = sp.DanhMuc?.TenDanhMuc ?? "",
                BienTheSanPham = sp.BienThes.Select(bt => new BienTheSanPhamDTO
                {
                    bienthe = new BienTheSPDTO
                    {
                        Gia = bt.Gia,
                        SoLuong = bt.SoLuong,
                        SKU = bt.SKU,
                        HinhAnhBienThe = bt.HinhAnh,
                    },
                    GiaTriBienTheSanPham = bt.ChiTietBienThes.Select(ct => new GiaTriBienTheSanPhamDto
                    {
                        TenGiaTri = ct.GiaTri?.TenGiaTri,
                        TenThuocTinh = ct.GiaTri?.ThuocTinh?.TenThuocTinh
                    }).ToList()
                }).ToList()
            }).ToList();

            return products;
        }


        public async Task<List<ProductDetail>> GetDatas()
        {
            var sanPhams = await _context.SanPham
                .Include(sp => sp.BienThes)
                    .ThenInclude(bt => bt.ChiTietBienThes)
                        .ThenInclude(ct => ct.GiaTri)
                            .ThenInclude(gt => gt.ThuocTinh)
                .Include(sp => sp.DanhMuc)
                .ToListAsync();

            var products = sanPhams.Select(sp => new ProductDetail
            {
                SanPham = new SanPhamDTO
                {
                    PathDanhMuc = sp.DanhMuc.Path,
                    IDCuaHang = sp.IDCuaHang,
                    TenSanPham = sp.TenSanPham,
                    MoTa = sp.MoTa,
                    TrangThai = sp.TrangThai,
                    NgayTao = sp.NgayTao
                },
                Images = sp.Images.Select(x => x.HinhAnh).ToList()
                ,
                DanhMuc = sp.DanhMuc?.TenDanhMuc ?? "",
                BienTheSanPham = sp.BienThes.Select(bt => new BienTheSanPhamDTO
                {
                    bienthe = new BienTheSPDTO
                    {
                        Gia = bt.Gia,
                        SoLuong = bt.SoLuong,
                        SKU = bt.SKU,
                        HinhAnhBienThe = bt.HinhAnh,
                    },
                    GiaTriBienTheSanPham = bt.ChiTietBienThes.Select(ct => new GiaTriBienTheSanPhamDto
                    {
                        TenGiaTri = ct.GiaTri?.TenGiaTri,
                        TenThuocTinh = ct.GiaTri?.ThuocTinh?.TenThuocTinh
                    }).ToList()
                }).ToList()
            }).ToList();

            return products;
        }


        public async Task<bool> PutBienTheByID(BienTheSPDTO type,string variantsId)
        {
            var bt = await _context.BienTheSanPham.FirstOrDefaultAsync(x=>x.IDBienTheSanPham == variantsId);
            if (bt == null) {
                return false;
            }
            bt.HinhAnh = type.HinhAnhBienThe;
            bt.Gia = type.Gia;
            bt.SKU = type.SKU;
            bt.SoLuong = type.SoLuong;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutData(SanPhamDTO type, string id)
        {
            var exists = await _context.SanPham.FirstOrDefaultAsync(x=>x.IDSanPham == id);
            if (exists == null)
            {
                return false;
            }
            var danhmuc = await _context.DanhMuc.FirstOrDefaultAsync(x=>x.Path == type.PathDanhMuc);
            exists.IDDanhMuc = danhmuc.IDDanhMuc;
            exists.MoTa = type.MoTa;
            exists.TenSanPham = type.TenSanPham;    
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutReview(ProductReview type,string id)
        {
            var exists = await _context.SanPham.FirstOrDefaultAsync(x => x.IDSanPham == id);
            if (exists == null)
            {
                return false;
            }
            var bt = await _context.BienTheSanPham.Where(x => x.IDSanPham == exists.IDSanPham).ToListAsync();
            if (!bt.Any())
            {
                return false;
            }
            var danhmuc =  await _context.DanhMuc.FirstOrDefaultAsync(x=> x.Path == type.PathDanhMuc);
            exists.IDDanhMuc = danhmuc.IDDanhMuc;
            exists.MoTa = type.MoTa;
            exists.TenSanPham = type.TenSanPham;
            danhmuc.Path = type.PathDanhMuc;
            var listdto = type.BienTheSanPham;
            foreach (var item in listdto)
            {
                var bienThe = item.bienthe;
                var thongtin = item.GiaTriBienTheSanPham;
                var existsbt = bt.FirstOrDefault(x => x.IDBienTheSanPham == type.IDBienTheSanPham);
                if (existsbt == null)
                {
                    continue;
                }
                existsbt.SKU = bienThe.SKU;
                existsbt.Gia = bienThe.Gia;
                existsbt.SoLuong = bienThe.SoLuong;
                foreach (var ttitem in thongtin)
                {
                    var gt = _context.GiaTriBTSP.FirstOrDefault(x => x.TenGiaTri == ttitem.TenGiaTri);
                    if (gt == null) continue;

                    var tt = _context.ThuocTinhBTSP.FirstOrDefault(x => x.ID == gt.IDThuocTinh);
                    if (tt == null) continue;
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

        public async Task<int> UpdateStatus(Guid id)
        {
            var exists = await _context.ChiTietDVVanChuyen.FirstOrDefaultAsync(x => x.ID == id);
            if (exists == null) { return 404};

            exists.TrangThaiSuDung = !exists.TrangThaiSuDung;

            await _context.SaveChangesAsync();
            return 200;
        }

    }
}
