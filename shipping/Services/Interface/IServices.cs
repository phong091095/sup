using shipping.Model;
using shipping.Model.DTO;

namespace shipping.Services.Interface
{
    public interface IShipServices<Type>
    {
        Task<IEnumerable<Type>> GetDatas();
        Task<string> UpdateData(DonViVanChuyenDTO type,string id);
        Task<bool> UpdatePatchData(string id);
    }
    //CREATE
    
    public interface IPostDTO<Type>
    {
        Task<Type> CreateData(Type type,string id);
    }
    public interface IPostDVVC<Type>
    {
        Task<Type> CreateDVVC(Type type);
        Task<bool> CreateCTVC(List<ChiTietDVVanChuyenDTO> type);
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
        Task<bool> PutBienTheByID(Type type,string id);
        Task<bool> PutChiTietBTByID(string id,GiaTriBienTheSanPhamDto type);
    }
    public interface IPutSp<Type>
    {
        Task<bool> PutData(Type type, string id);
    }
    public interface IPutReview<Type>
    {
        Task<bool> PutReview(ProductReview detail,string id);
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
        Task<bool> AddImageByID(string id, List<byte[]> images);
    }
    public interface IDeleteImage
    {
        Task<bool> DeleteImageByID(List<int> Idimage);
    }
    
    //StoreInterface
    public interface IGetAllStore
    {
        Task<List<CuaHang>> GetAll();
        Task<CuaHang> GetByID(string id);
    }
    public interface IPutApprove
    {
        Task<int> PutApprove(string id);
    }
    public interface IRejectStore
    {
        Task<int> RejectStore(string id);
    }
}
