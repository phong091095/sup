using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;
using WebAPI.Services;
using static shipping.Model.TrangThaiTong;

namespace shipping.Services.Implement
{
    public class StoreSvc : IGetAllStore, IPutApprove
    {
        private readonly Context _context;
        private readonly SendMail sendmail;
        public StoreSvc(Context context, SendMail sendmail)
        {
            _context = context;
            this.sendmail = sendmail;
        }
        public async Task<List<CuaHang>> GetAll()
        {
            var res = await _context.CuaHang.ToListAsync();
            return res;
        }
        public async Task<CuaHang> GetByID(string id)
        {
            var exists = await _context.CuaHang.FirstOrDefaultAsync(x => x.IDCuaHang == id);
            if (exists == null)
            {
                return new CuaHang();
            }
            return exists;
        }

        public async Task<int> PutApprove(string id)
        {
            var exists = await _context.CuaHang.FirstOrDefaultAsync(x => x.IDCuaHang == id);
            if (exists == null)
            {
                return 404;
            }
            if(exists.TrangThai == TrangThaiTong.StoreStatus.Pending)
            {
                exists.TrangThai = TrangThaiTong.StoreStatus.HoatDong;
                await _context.SaveChangesAsync();
                return 200;
            }
                return 500; 
        }

        //public Task<int> RejectStore(string id)
        //{
            
        //}

    }
}
