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
}
