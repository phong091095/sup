using shipping.Model;
using shipping.Model.DTO;

namespace shipping.Services.Interface
{
    public interface IBienThe
    {
        Task<bool> DeleteData(string id);
        Task<BResponse> CreateData(BienTheSPDTO type, string id);
        Task<bool> PutGT(GiaTriBienTheSanPhamDto dto, int id);
        Task<List<BienTheSanPham>> GetDatasById(string id);
    }
}
