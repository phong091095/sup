using shipping.Model;

namespace shipping.Services.Interface
{
    public interface IStore
    {
            Task<List<CuaHang>> GetAll();
            Task<CuaHang> GetByID(string id);
            Task<int> PutApprove(string id);
            Task<int> RejectStore(string id, string lydo);
            Task<string> GetStoreStatusByID(string id);
            Task<int> PutStoreStatus(string id);
            Task<bool> LockStore(string id, string lydo);
            Task<List<LogActiveties>> GetAllActive();
            Task<bool> SendNotify(string id, string tieuDe, string noiDung);
    }
}
