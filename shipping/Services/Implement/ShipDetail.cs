using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class ShipDetail : IPostDTO<ChiTietDVVanChuyen>
    {
        private readonly Context _context;
        public ShipDetail(Context context)
        {
            _context = context;
        }
        public async Task<ChiTietDVVanChuyen> CreateData(ChiTietDVVanChuyen type)
        {
            var exists = _context.DonViVanChuyen.FirstOrDefault(x=>x.IDDonViVanChuyen == type.IDDonViVanChuyen);
            if (exists == null) {
                return null;
            }
            _context.ChiTietDVVanChuyen.Add(type);
            await _context.SaveChangesAsync();
            return type;
        }
    }
}
