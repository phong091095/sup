using shipping.Model;
using shipping.Model.DTO;

namespace shipping.Services.Interface
{
    public interface IShipServices<Type>
    {
        Task<IEnumerable<Type>> GetDatas();
        Task<string> UpdateData(Type type);
        Task<bool> UpdatePatchData(string id);
    }
    //CREATE
    
    public interface IPostDTO<Type>
    {
        Task<Type> CreateData(Type type);
    }

    //GETDATA
    public interface IGetDTO<Type>
    {
        Task<List<Type>> GetDatas();
    }
    public interface IShipDTO<Type>
    {
        Task<Type> GetDataByIds(string id);
    }
    public interface IGetDataByID<Type>
    {
        Task<Type> GetDataByIds(string id);
    }
    public interface IGetByRQ<Type>
    {
        Task<List<Type>> GetDatabyRQ(RequestbodyDTO rqbd);
    }
    public interface IGetAll<Type>
    {
        Task<List<Type>> GetDatasById(string id);
    }
    //PUTDATA
    public interface IPutData<Type>
    {
        Task<bool> PutData(Type type);
    }
    public interface IPutByID<Type>
    {
        Task<bool> PutBienTheByID(Type type);
        Task<bool> PutChiTietBTByID(string id,GiaTriBienTheSanPhamDto type);
    }
    public interface IPutSp<Type>
    {
        Task<bool> PutData(Type type);
    }
    public interface IPutReview<Type>
    {
        Task<bool> PutReview(ProductDetail detail);
    }
    public interface IPutGT
    {
        Task<bool> PutGT(GiaTriBienTheSanPhamDto dto, int id);
    }
    public interface IPutShip
    {
        Task<bool> PutShipping(ChiTietDVVanChuyenDTO dto, Guid id);
    }
    //DELETEDATA
    public interface IDeleTeDTO<Type>
    {
        Task<bool> DeleteData(string id);
    }
    //IMAGE
    public interface IAddImage
    {
        Task<bool> AddImageByID(string id, byte[] image);
    }
    

}
