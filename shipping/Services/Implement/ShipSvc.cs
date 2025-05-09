using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class ShipSvc : IShipServices<DonViVanChuyen>,IShipDTO<ChiTietDonViVanChuyenDTO>,IPostDTO<ChiTietDonViVanChuyenDTO>
    {
        private readonly Context _context;
        public ShipSvc(Context context) {
            _context = context;
        }
        public async Task<IEnumerable<DonViVanChuyen>> GetDatas()
        {
            var ds = await _context.DonViVanChuyen.ToListAsync();
            return ds;
        }
        public async Task<string> UpdateData(DonViVanChuyen type)
        {
            var ds = _context.DonViVanChuyen.FirstOrDefault(x=>x.IDDonViVanChuyen == type.IDDonViVanChuyen);
            string mess = "";
            if (ds == null)
            {
                mess = "Không tìm thấy thông tin đơn vị vận chuyển";
                return mess;
            }
            ds.TenDonVi = type.TenDonVi;
            ds.SoDienThoai = type.SoDienThoai;
            await _context.SaveChangesAsync();
            mess = "Cập nhật thành công";
            return mess;
        }
        public async Task<bool> UpdatePatchData(string id)
        {
            var ds = _context.DonViVanChuyen.FirstOrDefault(x=>x.IDDonViVanChuyen == id);
            if (ds == null) {
                return false;
            }
            ds.TrangThai = ds.TrangThai == TrangThai.HoatDong.ToString()
                    ? TrangThai.NgungHoatDong.ToString()
                    : TrangThai.HoatDong.ToString();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ChiTietDonViVanChuyenDTO?> GetDataByIds(string id)
        {
            var dvvc = await _context.DonViVanChuyen
                .FirstOrDefaultAsync(x => x.IDDonViVanChuyen == id);

            if (dvvc == null)
            {
                return null;
            }

            var dsChiTiet = await _context.ChiTietDVVanChuyen
                .Where(ct => ct.IDDonViVanChuyen == id)
                .ToListAsync();

            var result = new ChiTietDonViVanChuyenDTO
            {
                dvvc = dvvc,
                dsdetail = dsChiTiet
            };

            return result;
        }


        public async Task<ChiTietDonViVanChuyenDTO> CreateData(ChiTietDonViVanChuyenDTO type)
        {
            DonViVanChuyen dvvc = type.dvvc;
            List<ChiTietDVVanChuyen> chitiet = type.dsdetail;
            int count = _context.DonViVanChuyen.Count() + 1;
            dvvc.IDDonViVanChuyen = "DVGH" + count;
            foreach (var item in chitiet) {
                item.IDDonViVanChuyen = dvvc.IDDonViVanChuyen;
            }
            _context.DonViVanChuyen.Add(dvvc);
            _context.ChiTietDVVanChuyen.AddRange(chitiet);
            await _context.SaveChangesAsync();
            return type;
        }

        
    }
}
