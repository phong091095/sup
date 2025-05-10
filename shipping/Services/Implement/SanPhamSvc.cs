using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class SanPhamSvc : IGetDTO<ProductDetail>,IGetByRQ<ProductDetail>
    {
        private readonly Context _context;
        public SanPhamSvc(Context context)
        {
            _context = context;
        }

        public Task<List<ProductDetail>> GetDatabyRQ(RequestbodyDTO rqbd)
        {
           public Task<List<ProductDetail>> GetDatas(RequestbodyDTO request)
        {
            var sanPhams = _context.SanPham.ToList();
            var danhMucs = _context.DanhMuc.ToList();
            var phanLoais = _context.PhanLoaiDanhMuc.ToList();
            var bienThes = _context.BienTheSanPham.ToList();
            var chiTietBienThes = _context.ChiTietBienTheSanPham.ToList();
            var thuocTinhs = _context.ThuocTinhBTSP.ToList();
            var giaTris = _context.GiaTriBTSP.ToList();

            // --- Áp dụng bộ lọc
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
                var product = new ProductDetail
                {
                    SanPham = sp,
                    DanhMuc = danhMucs.FirstOrDefault(d => d.IDDanhMuc == sp.IDDanhMuc),
                    PhanLoai = phanLoais.FirstOrDefault(p => p.Id == sp.IDPhanLoaiDanhMuc),
                    BienTheSanPham = bienThes
                        .Where(bt => bt.IDSanPham == sp.IDSanPham)
                        .Select(bt => new BienTheSanPhamDTO
                        {
                            bienthe = bt,
                            GiaTriBienTheSanPham = chiTietBienThes
                                .Where(ct => ct.IDBienTheSanPham == bt.IDBienTheSanPham)
                                .Select(ct =>
                                {
                                    var giaTri = giaTris.FirstOrDefault(g => g.ID == ct.IDGiaTriBienTheSanPham);
                                    var thuocTinh = thuocTinhs.FirstOrDefault(t => t.ID == giaTri.IDThuocTinh);

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
                var product = new ProductDetail
                {
                    SanPham = sp,
                    DanhMuc = danhMucs.FirstOrDefault(d => d.IDDanhMuc == sp.IDDanhMuc),
                    PhanLoai = phanLoais.FirstOrDefault(p => p.Id == sp.IDPhanLoaiDanhMuc),
                    BienTheSanPham = bienThes
                        .Where(bt => bt.IDSanPham == sp.IDSanPham)
                        .Select(bt => new BienTheSanPhamDTO
                        {
                            bienthe = bt,
                            GiaTriBienTheSanPham = chiTietBienThes
                                .Where(ct => ct.IDBienTheSanPham == bt.IDBienTheSanPham)
                                .Select(ct =>
                                {
                                    var giaTri = giaTris.FirstOrDefault(g => g.ID == ct.IDGiaTriBienTheSanPham);
                                    var thuocTinh = thuocTinhs.FirstOrDefault(t => t.ID == giaTri.IDThuocTinh);

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

    }
}
