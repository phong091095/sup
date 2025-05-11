using shipping.Model;

namespace shipping.Services.Interface
{
    public interface IShipServices<Type>
    {
        Task<IEnumerable<Type>> GetDatas();
        Task<string> UpdateData(Type type);
        Task<bool> UpdatePatchData(string id);
    }
    public interface IShipDTO<Type>
    {
        Task<Type> GetDataByIds(string id);
    }
    public interface IPostDTO<Type>
    {
        Task<Type> CreateData(Type type);
    }
    public interface IGetDTO<Type>
    {
        Task<List<Type>> GetDatas();
    }
    public interface IGetByRQ<Type>
    {
        Task<List<Type>> GetDatabyRQ(RequestbodyDTO rqbd);
    }
    public interface IGetByID<Type>
    {
        Task<Type> GetDataByIds(string id);
    }
   public interface IPutData<Type>
    {
        Task<bool> PutData(Type type);
    }
    
    public interface IPutByID<Type>
    {
        Task<bool> PutBienTheByID(string id,Type type);
        Task<bool> PutChiTietBTByID(string id,GiaTriBienTheSanPhamDto type);
    }
    public interface IDeleTeDTO<Type>
    {
        Task<bool> DeleteData(string id);
    }
}
