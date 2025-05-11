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
    public interface IGetDataByID<Type>
    {
        Task<Type> GetDataByIds(string id);
    }
   public interface IPutData<Type>
    {
        Task<bool> PutData(Type type);
    }
    
    public interface IPutByID<Type>
    {
        Task<bool> PutBienTheByID(Type type);
        Task<bool> PutChiTietBTByID(string id,GiaTriBienTheSanPhamDto type);
    }
    public interface IDeleTeDTO<Type>
    {
        Task<bool> DeleteData(string id);
    }
    public interface IPutSp<Type>
    {
        Task<bool >PutData(Type type);
    }
    public interface IAddImage
    {
        Task<bool> AddImageByID(string id, byte[] image);
    }
    public interface IGetAll<Type>
    {
        Task<List<Type>> GetDatasById(string id);
    }

}
