using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Model.DTO;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class ShipSvc : IShipServices<DonViVanChuyen>,IPostDTO<DonViVanChuyen>,IShipDTO<ChiTietDonViVanChuyenDTO>, IPutShip
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
            ds.TrangThai = ds.TrangThai == TrangThaiTong.TrangThai.HoatDong.ToString()
                    ? TrangThaiTong.TrangThai.NgungHoatDong.ToString()
                    : TrangThaiTong.TrangThai.HoatDong.ToString();
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


        public async Task<DonViVanChuyen> CreateData(DonViVanChuyen type)
        {
            int count = _context.DonViVanChuyen.Count() + 1;
            type.IDDonViVanChuyen = "DVGH" + count;
            _context.DonViVanChuyen.Add(type);
            await _context.SaveChangesAsync();
            return type;
        }


        public async Task<bool> PutShipping(ChiTietDVVanChuyenDTO type, Guid id)
        {
            var exists = await _context.ChiTietDVVanChuyen.FirstOrDefaultAsync(x => x.ID == id);
            if (exists == null)
            {
                return false;
            }
            exists.PhiVanChuyen = type.PhiVanChuyen;
            exists.NgayCapNhat = DateTime.Now;
            exists.ThoiGianDuKien = type.ThoiGianDuKien;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
