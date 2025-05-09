using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class ShipSvc : IShipServices<DonViVanChuyen>,IShipDTO<ChiTietDonViVanChuyenDTO>
    {
        private readonly Context _context;
        public ShipSvc(Context context) {
            _context = context;
        }
        public async Task<DonViVanChuyen> CreateData(DonViVanChuyen type)
        {
            int count = _context.DonViVanChuyen.Count() + 1;
            type.IDDonViVanChuyen = "GHN" + count;
            _context.DonViVanChuyen.Add(type);
            await _context.SaveChangesAsync();
            return type; 
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
            ds.TrangThai = "NgungHoatDong";
            await _context.SaveChangesAsync();
            return true;
        }
        
        public  Task<DonViVanChuyen> GetDataById(string id)
        {
            throw new NotImplementedException();

        }

        public async Task<IEnumerable<ChiTietDonViVanChuyenDTO>> GetDataByIds(string id)
        {
            var result = await _context.ChiTietDVVanChuyen
                .Where(ct => ct.IDDonViVanChuyen == id)
                .Join(_context.DonViVanChuyen,
                      ct => ct.IDDonViVanChuyen,
                      dv => dv.IDDonViVanChuyen,
                      (ct, dv) => new ChiTietDonViVanChuyenDTO
                      {
                          ID = ct.ID,
                          IDCuaHang = ct.IDCuaHang,
                          IDDonViVanChuyen = ct.IDDonViVanChuyen,
                          PhiVanChuyen = ct.PhiVanChuyen,
                          ThoiGianDuKien = ct.ThoiGianDuKien,
                          NgayCapNhat = ct.NgayCapNhat,

                          TenDonVi = dv.TenDonVi,
                          SoDienThoai = dv.SoDienThoai,
                          Email = dv.Email,
                          MoTa = dv.MoTa,
                          TrangThai = dv.TrangThai
                      })
                .ToListAsync();

            return result;
        }
    }
}
