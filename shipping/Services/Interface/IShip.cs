using shipping.Model;
using shipping.Model.DTO;

namespace shipping.Services.Interface
{
    public interface IShip
    {
            Task<IEnumerable<DonViVanChuyen>> GetDatas();
            Task<string> UpdateData(DonViVanChuyenDTO type, string id);
            Task<bool> UpdatePatchData(string id);
            Task<DonViVanChuyenDTO> CreateDVVC(DonViVanChuyenDTO type);
            Task<bool> CreateCTVC(List<ChiTietDVVanChuyenDTO> type);
            Task<ChiTietDonViVanChuyenDTO> GetDataByIds(string id);
            Task<bool> PutShipping(ChiTietDVVanChuyenDTO dto, Guid id);
    }
}
