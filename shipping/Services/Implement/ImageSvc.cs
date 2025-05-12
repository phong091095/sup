using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using shipping.Model;
using shipping.Services.Interface;

namespace shipping.Services.Implement
{
    public class ImageSvc : IAddImage, IDeleteImage
    {
        private readonly Context _context;
        public ImageSvc(Context context)
        {
            _context = context;
        }

        public async Task<bool> AddImageByID(string id, List<byte[]> images)
        {
            if (!await _context.SanPham.AnyAsync(x => x.IDSanPham == id))
                return false;

            int existingCount = await _context.Images.CountAsync(x => x.IDSanPham == id);
            if (existingCount + images.Count > 9)
                return false;

            var newImages = images.Select(img => new Images
            {
                HinhAnh = img,
                IDSanPham = id
            });

            await _context.Images.AddRangeAsync(newImages);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteImageByID(List<int> Idimage)
        {
            var images = await _context.Images
                .Where(img => Idimage.Contains(img.Id))
                .ToListAsync();

            if (images.Any())
            {
                _context.Images.RemoveRange(images);
                await _context.SaveChangesAsync();
            }

            return true;
        }

    }
}
