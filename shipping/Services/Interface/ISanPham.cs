using shipping.Model;
using shipping.Model.DTO;

namespace shipping.Services.Interface
{
    public interface ISanPham
    {
            Task<List<ProductDetail>> GetDatas();
            //Task<List<Type>> GetDatabyRQ(RequestbodyDTO rqbd);
            Task<bool> DeleteData(string id);
            Task<bool> PutReview(string id);
            Task<bool> PutData(SanPhamDTO type, string id);
            Task<int> UpdateStatus(Guid id);
    }
}
