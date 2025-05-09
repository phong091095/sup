namespace shipping.Services.Interface
{
    public interface IShipServices<Type>
    {
        Task<IEnumerable<Type>> GetDatas();
        Task<Type> GetDataById(string id);
        Task<Type> CreateData(Type type);
        Task<string> UpdateData(Type type);
        Task<bool> UpdatePatchData(string id);
    }
    public interface IShipDTO<Type>
    {
        Task<IEnumerable<Type>> GetDataByIds(string id);
    }
}
