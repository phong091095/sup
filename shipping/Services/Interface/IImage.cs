namespace shipping.Services.Interface
{
    public interface IImage
    {
            Task<bool> AddImageByID(string id, List<byte[]> images);
            Task<bool> DeleteImageByID(List<int> Idimage);
    }
}
