using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;
using WebAPI.Services;
using static shipping.Model.TrangThaiTong;

namespace shipping.Services.Implement
{
    public class StoreSvc : IGetAllStore, IPutApprove, IRejectStore, IGetStoreStatus, IPutStoreStatus,ILockStore,IGetActive, ISendNotify
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

        public async Task<List<LogActiveties>> GetAllActive()
        {
            var res = await _context.LogActiveties.ToListAsync();
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

        public async Task<string> GetStoreStatusByID(string id)
        {
            var exists = await _context.CuaHang.FirstOrDefaultAsync(x => x.IDCuaHang == id);
            if(exists == null)
            {
                return "Không tìm thấy thông tin cửa hàng. Vui lòng kiểm tra lại mã.";
            }
            return exists.TrangThai.ToString();
        }

        public async Task<bool> LockStore(string id,string lydo)
        {
            var exists = await _context.CuaHang.FirstOrDefaultAsync(x => x.IDCuaHang == id);
            if (exists == null)
            {
                return false;
            }
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.ID == exists.ID);
            if (user == null)
            {
                return false;
            }
            exists.TrangThai = StoreStatus.NgungHoatDong.ToString();
            LogActiveties newlog = new LogActiveties
            {
                UserName = user.UserName,
                Action = "Khóa cửa hàng"
            };
            _context.LogActiveties.Add(newlog);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(lydo))
            {
                var res = sendmail.SendEmail(user.Email, lydo);
                if (!res)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<int> PutApprove(string id)
        {
            var exists = await _context.CuaHang.FirstOrDefaultAsync(x => x.IDCuaHang == id);
            if (exists == null)
            {
                return 404;
            }
            if(exists.TrangThai == TrangThaiTong.StoreStatus.Pending.ToString())
            {
                exists.TrangThai = TrangThaiTong.StoreStatus.HoatDong.ToString();
                await _context.SaveChangesAsync();
                return 200;
            }
                return 500; 
        }

        public async Task<int> PutStoreStatus(string id)
        {
            var exists = await _context.CuaHang.FirstOrDefaultAsync(x => x.IDCuaHang == id);
            if (exists == null)
            {
                return 404;
            }
            exists.TrangThai = TrangThaiTong.StoreStatus.HoatDong.ToString();
            await _context.SaveChangesAsync();
            return 200;
        }

        public async Task<int> RejectStore(string id, string lydo)
        {
            var exists = await _context.CuaHang.FirstOrDefaultAsync(x => x.IDCuaHang == id);
            if (exists == null)
            {
                return 404;
            }
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.ID == exists.ID);
            if (user == null)
            {
                return 404;
            }
            if (exists.TrangThai == StoreStatus.Pending.ToString())
            {
                var res = sendmail.SendRejectEmail(user.Email, lydo);
                if (res)
                {
                    exists.TrangThai = StoreStatus.Reject.ToString();
                    LogActiveties newlog = new LogActiveties
                    {
                        UserName = user.UserName,
                        Action = "Từ chối đăng ký cửa hàng."
                    };
                    _context.LogActiveties.Add(newlog);
                    await _context.SaveChangesAsync();
                    return 200;
                }
                else
                {
                    return 400;
                }
            }
            else
            {
                return 409;
            }
           
        }

        public async Task<bool> SendNotify(string id, string tieuDe, string noiDung)
        {
            var exists = await _context.CuaHang.FirstOrDefaultAsync(x => x.IDCuaHang == id);
            if (exists == null)
            {
                return false;
            }
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.ID == exists.ID);
            if (user == null)
            {
                return false;
            }
            var res = sendmail.SendNotify(user.Email, tieuDe, noiDung);
            return res;
        }
    }
}
