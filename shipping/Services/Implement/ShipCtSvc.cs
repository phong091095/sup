using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class ShipCtSvc : IShipServices<ChiTietDVVanChuyen>
    {
        private readonly Context _context;
        public ShipCtSvc(Context context)
        {
            _context = context;
        }
        public async Task<ChiTietDVVanChuyen> CreateData(ChiTietDVVanChuyen type)
        {
            var dv = _context.DonViVanChuyen.FirstOrDefault(x=> x.IDDonViVanChuyen == type.IDDonViVanChuyen);
            if (dv == null)
            {
                return null;
            }
            type.NgayCapNhat = DateTime.Now;
            _context.ChiTietDVVanChuyen.Add(type);
            await _context.SaveChangesAsync();
            return type;
        }

        public Task<ChiTietDVVanChuyen> GetDataById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChiTietDVVanChuyen>> GetDatas()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateData(ChiTietDVVanChuyen type)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePatchData(string id)
        {
            throw new NotImplementedException();
        }
    }
}
