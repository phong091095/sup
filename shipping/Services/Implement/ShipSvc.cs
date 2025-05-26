using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Model.DTO;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class ShipSvc : IShip
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
        public async Task<string> UpdateData(DonViVanChuyenDTO type,string id)
        {
            var ds = _context.DonViVanChuyen.FirstOrDefault(x=>x.IDDonViVanChuyen == id);
            string mess = "";
            if (ds == null)
            {
                mess = "Không tìm thấy thông tin đơn vị vận chuyển";
                return mess;
            }
            ds.TenDonVi = type.TenDonVi;
            ds.SoDienThoai = type.SoDienThoai;
            ds.Email = type.Email;
            ds.MoTa = type.MoTa;
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
            List<ChiTietDVVanChuyenDTO> detail = new List<ChiTietDVVanChuyenDTO>();
            foreach(var item in dsChiTiet)
            {
                var dt = new ChiTietDVVanChuyenDTO();
                dt.PhiVanChuyen = item.PhiVanChuyen;
                dt.IDDonViVanChuyen = item.IDDonViVanChuyen;
                dt.NgayCapNhat = item.NgayCapNhat;
                dt.IDCuaHang = item.IDCuaHang;
                dt.ThoiGianDuKien = item.ThoiGianDuKien;
                detail.Add(dt);
            }
            var result = new ChiTietDonViVanChuyenDTO
            {
                dvvc = dvvc,
                dsdetail = detail
            };

            return result;
        }


        public async Task<DonViVanChuyenDTO> CreateDVVC(DonViVanChuyenDTO type)
        {
            DonViVanChuyen dvvc = new DonViVanChuyen();
            int count = _context.DonViVanChuyen.Count() + 1;
            dvvc.IDDonViVanChuyen = "DVGH" + count;
            dvvc.TenDonVi = type.TenDonVi;
            dvvc.MoTa = type.MoTa;
            dvvc.Email = type.Email;
            dvvc.SoDienThoai = type.SoDienThoai;
            dvvc.TrangThai = type.TrangThai;
            _context.DonViVanChuyen.Add(dvvc);
            await _context.SaveChangesAsync();
            return type;
        }
        public async Task<bool> CreateCTVC(List<ChiTietDVVanChuyenDTO> type)
        {
            foreach (var item in type) {
                ChiTietDVVanChuyen ct = new ChiTietDVVanChuyen();
                ct.ID = Guid.NewGuid();
                ct.PhiVanChuyen = item.PhiVanChuyen;
                ct.IDDonViVanChuyen = item.IDDonViVanChuyen;
                ct.NgayCapNhat = DateTime.Now;
                ct.ThoiGianDuKien = item.ThoiGianDuKien;
                _context.ChiTietDVVanChuyen.Add(ct);
            }
           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutShipping(ChiTietDVVanChuyenDTO type, Guid id)
        {
            var exists = await _context.ChiTietDVVanChuyen.FirstOrDefaultAsync(x => x.ID == id);
            if (exists == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(type.IDCuaHang))
            {
                exists.IDCuaHang = type.IDCuaHang;
            }
            else
            {
                exists.IDCuaHang = null;
            }
            exists.PhiVanChuyen = type.PhiVanChuyen;
            exists.NgayCapNhat = DateTime.Now;
            exists.ThoiGianDuKien = type.ThoiGianDuKien;
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}
