using CategoriesService.DTO;
using CategoriesService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shipping.DBContext;
using static System.Net.Mime.MediaTypeNames;

namespace CategoriesService.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly Context _context;
        private readonly IBadWordService _badWordService;

        public CategoryService(Context context, IBadWordService badWordService)
        {
            _context = context;
            _badWordService = badWordService;
        }
        public async Task<int> CreateIDDanhMuc()
        {
            var maxId = _context.DanhMucs
                .Select(c => c.IDDanhMuc)
                .AsEnumerable()
                .DefaultIfEmpty(0)
                .Max();


            int nextId = maxId + 1;

            while (await _context.DanhMucs.AnyAsync(c => c.IDDanhMuc == nextId))
            {
                nextId++;
            }

            return nextId;
        }

        public async Task<ResponseDTO> CreateCategoryLvl1Async(CreateCategoryLvl1DTO dto)
        {
            try
            {
                var (isBad, badWords) = await _badWordService.CheckProfanityAsync(dto.TenDanhMuc);
                if (isBad)
                {
                    return new ResponseDTO
                    {
                        Status = "error",
                        Message = $"Tên danh mục chứa từ nhạy cảm: {string.Join(", ", badWords)}"
                    };
                }

                bool categoryExists = await _context.DanhMucs
                    .AnyAsync(c => c.TenDanhMuc.ToLower() == dto.TenDanhMuc.ToLower());

                if (categoryExists)
                {
                    return new ResponseDTO
                    {
                        Status = "error",
                        Message = "Tên danh mục đã tồn tại."
                    };
                }

                int IDDanhMuc = await CreateIDDanhMuc();

                var category = new DanhMuc
                {
                    IDDanhMuc = IDDanhMuc,
                    TenDanhMuc = dto.TenDanhMuc,
                    CapDanhMuc = 1,
                    Path = IDDanhMuc.ToString(),
                    TrangThai = true,
                    IsLeaf = true
                };

                _context.DanhMucs.Add(category);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new ResponseDTO
                    {
                        Status = "success",
                        Message = "Danh mục đã được thêm thành công."
                    };
                }

                return new ResponseDTO
                {
                    Status = "error",
                    Message = "Có lỗi xảy ra khi thêm danh mục."
                };
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseDTO
                {
                    Status = "error",
                    Message = "Có lỗi xảy ra khi cập nhật cơ sở dữ liệu: " + dbEx.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = "error",
                    Message = "Có lỗi không xác định: " + ex.Message
                };
            }
        }
        public async Task<ResponseCategoryList> GetListCategoryLvl1Async()
        {
            try
            {
                // Giả định bạn có xác thực và đang lấy UserID hoặc quyền từ context
                // bool hasPermission = CheckUserPermission(); // nếu bạn có chức năng phân quyền
                // if (!hasPermission)
                // {
                //     return new ResponseCategoryList
                //     {
                //         StatusCode = 401,
                //         Status = "unauthorized",
                //         Message = "Bạn không có quyền thực hiện thao tác này."
                //     };
                // }

                var categories = await _context.DanhMucs
                    .Where(c => c.CapDanhMuc == 1)
                    .Select(c => new ResponseCategory
                    {
                        TenDanhMuc = c.TenDanhMuc,
                        IDDanhMuc = c.IDDanhMuc
                    })
                    .ToListAsync();

                if (categories == null || !categories.Any())
                {
                    return new ResponseCategoryList
                    {
                        StatusCode = 204,
                        Status = "no_content",
                        Message = "Không có danh mục nào được tìm thấy.",
                        Categories = new List<ResponseCategory>() // tránh null
                    };
                }

                return new ResponseCategoryList
                {
                    StatusCode = 200,
                    Status = "success",
                    Message = "Danh mục đã được lấy thành công.",
                    Categories = categories
                };
            }
            catch (Exception ex)
            {
                // Ghi log nếu cần
                return new ResponseCategoryList
                {
                    StatusCode = 500,
                    Status = "error",
                    Message = "Đã xảy ra lỗi trong quá trình xử lý: " + ex.Message
                };
            }
        }
        public async Task<ResponseDTO> CreateCategoryLvl2345Async(CreateCategoryLvl2345DTO dto)
        {
            try
            {
                var (isBad, badWords) = await _badWordService.CheckProfanityAsync(dto.TenDanhMuc);
                if (isBad)
                {
                    return new ResponseDTO
                    {
                        Status = "error",
                        Message = $"Tên danh mục chứa từ nhạy cảm: {string.Join(", ", badWords)}"
                    };
                }

                bool categoryExists = await _context.DanhMucs
                    .AnyAsync(c => c.TenDanhMuc.ToLower() == dto.TenDanhMuc.ToLower());

                if (categoryExists)
                {
                    return new ResponseDTO
                    {
                        Status = "error",
                        Message = "Tên danh mục đã tồn tại."
                    };
                }

                bool idCategoryExists = await _context.DanhMucs
                    .AnyAsync(n => n.IDDanhMuc == dto.IDDanhMuc);

                if (!idCategoryExists)
                {
                    return new ResponseDTO
                    {
                        Status = "error",
                        Message = $"Danh mục Cấp {dto.CapDanhMuc} với ID {dto.IDDanhMuc} không tồn tại."
                    };
                }

                //bool parentHasImage = await _context.HinhAnhDanhMucs
                //    .AnyAsync(img => img.IDDanhMuc == dto.IDDanhMuc);

                //if (parentHasImage)
                //{
                //    if (!dto.ForceOverrideImage)
                //    {
                //        return new ResponseDTO
                //        {
                //            Status = "warning", 
                //            Message = "Danh mục cha đã có ảnh. Nếu tiếp tục thêm danh mục con thì ảnh sẽ bị xóa. Bạn có muốn tiếp tục?"
                //        };
                //    }
                //    else
                //    {
                //        var imagesToDelete = await _context.HinhAnhDanhMucs
                //            .Where(img => img.IDDanhMuc == dto.IDDanhMuc)
                //            .ToListAsync();

                //        _context.HinhAnhDanhMucs.RemoveRange(imagesToDelete);
                //        await _context.SaveChangesAsync();
                //    }
                //}

                int IDDanhMuc = await CreateIDDanhMuc();

                var parentPath = await _context.DanhMucs
                            .Where(c => c.IDDanhMuc == dto.IDDanhMuc)
                            .Select(c => c.Path)
                            .FirstOrDefaultAsync();

                string categoryPath = $"{parentPath}/{IDDanhMuc}";


                if (dto.CapDanhMuc == 5)
                {
                    dto.IsLeaf = true;
                }
                var category = new DanhMuc
                {
                    IDDanhMuc = IDDanhMuc,
                    TenDanhMuc = dto.TenDanhMuc,
                    CapDanhMuc = dto.CapDanhMuc,
                    Path = categoryPath,
                    TrangThai = true,
                    IsLeaf = dto.IsLeaf,
                };

                _context.DanhMucs.Add(category);
                var result = await _context.SaveChangesAsync();

                if (dto.Images != null && dto.Images.Any())
                {
                    foreach (var imgPath in dto.Images)
                    {
                        _context.HinhAnhDanhMucs.Add(new HinhAnhDanhMuc
                        {
                            IDDanhMuc = IDDanhMuc,
                            HinhAnh = imgPath
                        });
                    }

                    await _context.SaveChangesAsync();
                }
                var categorydad = await _context.DanhMucs.FindAsync(dto.IDDanhMuc);
                if (categorydad != null)
                {
                    categorydad.IsLeaf = false;
                    _context.DanhMucs.Update(categorydad);
                    await _context.SaveChangesAsync();
                }
                

                if (result > 0)
                {
                    return new ResponseDTO
                    {
                        Status = "success",
                        Message = "Danh mục đã được thêm thành công."
                    };
                }

                return new ResponseDTO
                {
                    Status = "error",
                    Message = "Có lỗi xảy ra khi thêm danh mục."
                };
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseDTO
                {
                    Status = "error",
                    Message = "Có lỗi xảy ra khi cập nhật cơ sở dữ liệu: " + dbEx.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = "error",
                    Message = "Có lỗi không xác định: " + ex.Message
                };
            }
        }
        public async Task<ResponseCategoryList> GetListCategoryLvl2345Async(int Socap)
        {
            try
            {
                // Giả định bạn có xác thực và đang lấy UserID hoặc quyền từ context
                // bool hasPermission = CheckUserPermission(); // nếu bạn có chức năng phân quyền
                // if (!hasPermission)
                // {
                //     return new ResponseCategoryList
                //     {
                //         StatusCode = 401,
                //         Status = "unauthorized",
                //         Message = "Bạn không có quyền thực hiện thao tác này."
                //     };
                // }

                var categories = await _context.DanhMucs
                    .Where(c => c.CapDanhMuc == Socap)
                    .Select(c => new ResponseCategory
                    {
                        TenDanhMuc = c.TenDanhMuc,
                        IDDanhMuc = c.IDDanhMuc
                    })
                    .ToListAsync();

                if (categories == null || !categories.Any())
                {
                    return new ResponseCategoryList
                    {
                        StatusCode = 204,
                        Status = "no_content",
                        Message = "Không có danh mục nào được tìm thấy.",
                    };
                }

                return new ResponseCategoryList
                {
                    StatusCode = 200,
                    Status = "success",
                    Message = "Danh mục đã được lấy thành công.",
                    Categories = categories
                };
            }
            catch (Exception ex)
            {
                return new ResponseCategoryList
                {
                    StatusCode = 500,
                    Status = "error",
                    Message = "Đã xảy ra lỗi trong quá trình xử lý: " + ex.Message
                };
            }
        }

        public async Task<ResponseDTO> DeleteCategory(int IDDanhMuc)
        {
            var existscate = await _context.DanhMucs.FirstOrDefaultAsync(x => x.IDDanhMuc == IDDanhMuc);
            if (existscate == null)
            {
                return new ResponseDTO
                {
                    Message = "Không tìm thấy danh mục",
                    Status = "error"
                };
            }
            var danhmucs = _context.DanhMucs.Where(x=>!string.IsNullOrEmpty(x.Path) && x.CapDanhMuc == 2).ToList();
            bool haschild = danhmucs.Any(x =>
            {
                var parts = x.Path.Split('/');
                if(parts.Length >= 1 && int.TryParse(parts[0],out int parentId)){
                    return parentId == IDDanhMuc;
                }
                return false;
            });
            if (!haschild) {
                _context.DanhMucs.Remove(existscate);
                _context.SaveChanges();
                return new ResponseDTO
                {
                    Message = "Xóa danh mục thành công",
                    Status = "success"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    Message = "Không thể xóa danh mục này vì có danh mục con",
                    Status = "error"
                };
            }
        }

       public async Task<ResponseDTO> MoveCategory(int IDDanhMucmove, int IdDanhMuc)
        {
            if (IdDanhMuc == IDDanhMucmove)
            {
                return new ResponseDTO
                {
                    Message = "Không thể di chuyển danh mục vào chính nó.",
                    Status = "error"
                };
            }
            var danhMuc = await _context.DanhMucs.FirstOrDefaultAsync(x => x.IDDanhMuc == IdDanhMuc);
            var danhMucTarget = await _context.DanhMucs.FirstOrDefaultAsync(x => x.IDDanhMuc == IDDanhMucmove);

            if (IDDanhMucmove == 0)
            {
                danhMuc.CapDanhMuc = 1;
                danhMuc.Path = $"{IdDanhMuc}";
                danhMuc.IsLeaf = true;
                await _context.SaveChangesAsync();

                return new ResponseDTO
                {
                    Message = $"Đã đưa danh mục về cấp 1. Path mới: {danhMuc.Path}",
                    Status = "success"
                };
            }


            if (danhMuc == null || danhMucTarget == null)
            {
                return new ResponseDTO
                {
                    Message = "Không tìm thấy danh mục nguồn hoặc đích.",
                    Status = "error"
                };
            }
            if (!string.IsNullOrEmpty(danhMucTarget.Path))
            {
                var pathIds = danhMucTarget.Path.Split('/').Select(id => int.TryParse(id, out int result) ? result : -1).ToList();
                if (pathIds.Contains(IdDanhMuc))
                {
                    return new ResponseDTO
                    {
                        Message = "Không thể di chuyển danh mục vào chính nó hoặc hậu duệ của nó.",
                        Status = "error"
                    };
                }
            }
            bool hasChildren = await _context.DanhMucs.AnyAsync(dm =>
                               dm.Path.StartsWith($"{IdDanhMuc}/") ||
                               dm.Path.Contains($"/{IdDanhMuc}/") ||
                               dm.Path.EndsWith($"/{IdDanhMuc}")
                           );
            if (hasChildren)
            {
                return new ResponseDTO
                {
                    Message = "Không thể di chuyển danh mục đang có danh mục con.",
                    Status = "error"
                };
            }
            if (!danhMuc.IsLeaf)
            {
                return new ResponseDTO
                {
                    Message = "Chỉ danh mục không có con (IsLeaf=true) mới được phép di chuyển.",
                    Status = "error"
                };
            }
            int newCapDanhMuc = danhMucTarget.CapDanhMuc + 1;
            if (newCapDanhMuc > 5)
            {
                return new ResponseDTO
                {
                    Message = "Không thể chuyển danh mục vượt quá cấp 5.",
                    Status = "error"
                };
            }
            danhMuc.CapDanhMuc = newCapDanhMuc;
            danhMuc.Path = string.IsNullOrWhiteSpace(danhMucTarget.Path)
                ? $"{IdDanhMuc}"
                : $"{danhMucTarget.Path}/{IdDanhMuc}";

            danhMuc.IsLeaf = true;
            danhMucTarget.IsLeaf = false;
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Message = $"Di chuyển danh mục thành công. Path mới: {danhMuc.Path}",
                Status = "success"
            };
        }


        private DanhMucDTO BuildDanhMucDTO(DanhMuc danhMuc, List<DanhMuc> allDanhMucs)
        {
            return new DanhMucDTO
            {
                IdDanhMuc = danhMuc.IDDanhMuc,
                TenDanhMuc = danhMuc.TenDanhMuc,
                CapDanhMuc = danhMuc.CapDanhMuc,
                Path = danhMuc.Path ?? "",
                TrangThai = danhMuc.TrangThai,
                IsLeaf = danhMuc.IsLeaf,
                SoLuongSanPhamLienQuan = _context.SanPham.Count(sp => sp.IDDanhMuc == danhMuc.IDDanhMuc), 

                HinhAnhs = danhMuc.HinhAnhDanhMucs.Select(img => new HinhAnhDanhMucDTO
                {
                    IdHinhAnhDanhMuc = img.IDHinhAnhDanhMuc,
                    HinhAnh = img.HinhAnh 
                }).ToList(),

                Children = allDanhMucs
                    .Where(child => child.Path != null && child.Path.StartsWith($"{danhMuc.IDDanhMuc}/") && child.CapDanhMuc == danhMuc.CapDanhMuc + 1)
                    .Select(child => BuildDanhMucDTO(child, allDanhMucs))
                    .ToList()
            };
        }

        public async Task<List<DanhMucDTO>> GetCategories()
        {
            var allDanhMucs = await _context.DanhMucs
                            .Include(dm => dm.HinhAnhDanhMucs)
                            .ToListAsync();

            var rootDanhMucs = allDanhMucs.Where(dm => dm.CapDanhMuc == 1).ToList();

            var tree = rootDanhMucs.Select(dm => BuildDanhMucDTO(dm, allDanhMucs)).ToList();

            return tree;
        }

        public async Task<DanhMucDTO> GetCateByID(int idDanhMuc)
        {
            var allDanhMucs = await _context.DanhMucs
                .Include(dm => dm.HinhAnhDanhMucs)
                .ToListAsync();

            var root = allDanhMucs.FirstOrDefault(dm => dm.IDDanhMuc == idDanhMuc);

            if (root == null)
                return null;

            return BuildDanhMucDTO(root, allDanhMucs);
        }
        public async Task<List<HinhAnhDanhMuc>> GetImagesByID(int IDDanhMuc)
        {
            var images = await _context.HinhAnhDanhMucs.Where(x => x.IDDanhMuc == IDDanhMuc).ToListAsync();
            if (images.Any())
            {
                return images;
            }
            else
            {
                return new List<HinhAnhDanhMuc> { };
            }
        }

        public async Task<ResponseDTO> DeleteAllImages(int IDDanhMuc)
        {
            var images = await _context.HinhAnhDanhMucs.Where(x=>x.IDDanhMuc ==IDDanhMuc).ToListAsync();
            if (images.Any()) {
                _context.RemoveRange(images);
                await _context.SaveChangesAsync();
                return new ResponseDTO
                {
                    Message = "Xóa thành công hình ảnh của danh mục.",
                    Status = "success"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    Message = "Không tìm thấy hình ảnh với ID danh mục tương ứng",
                    Status = "error"
                };
            }
        }

        public async Task<ResponseDTO> DeleteImagesByID(int imageId)
        {
            var images = await _context.HinhAnhDanhMucs.FirstOrDefaultAsync(x =>x.IDHinhAnhDanhMuc == imageId);
            Console.WriteLine(imageId);
            if (images != null)
            {
                _context.Remove(images);
                await _context.SaveChangesAsync();
                return new ResponseDTO
                {
                    Message = "Xóa thành công hình ảnh của danh mục.",
                    Status = "success"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    Message = "Không tìm thấy hình ảnh với ID hình ảnh tương ứng",
                    Status = "error"
                };
            }
        }
        public async Task<ResponseDTO> UpdateStatus(int IDDanhMuc)
        {
            var res = await _context.DanhMucs.FirstOrDefaultAsync(x=>x.IDDanhMuc == IDDanhMuc);
            if (res != null) {
                res.TrangThai = !res.TrangThai;
                _context.SaveChanges();
                return new ResponseDTO
                {
                    Message = "Trạng thái danh mục đã được cập nhật thành công.",
                    Status = "success"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    Message = "Không tìm thấy danh mục với ID tương ứng",
                    Status = "error"
                };
            }
        }

        public async Task<ResponseDTO> UpdateLvl2345(int id, CreateCategoryLvl2345DTO dTO)
        {
            var exists = await _context.DanhMucs.FirstOrDefaultAsync(x => x.IDDanhMuc == id);
            if (exists == null)
            {
                return new ResponseDTO
                {
                    Message = "Không tìm thấy danh mục với ID tương ứng",
                    Status = "error"
                };
            }

            if(!exists.IsLeaf) {

                return new ResponseDTO
                {
                    Message = "Không thể cập nhật danh mục có danh mục con.",
                    Status = "error"
                };
            }

            int? oldParentId = null;
            if (!string.IsNullOrEmpty(exists.Path))
            {
                var parts = exists.Path.Split('/');
                if (parts.Length >= 1 && int.TryParse(parts[^2], out int parsedId)) 
                {
                    oldParentId = parsedId;
                }
            }

            var newParent = await _context.DanhMucs.FirstOrDefaultAsync(x => x.IDDanhMuc == dTO.IDDanhMuc);
            if (newParent == null)
            {
                return new ResponseDTO
                {
                    Message = "Không tìm thấy danh mục cha mới.",
                    Status = "error"
                };
            }

            exists.TenDanhMuc = dTO.TenDanhMuc;
            exists.CapDanhMuc = dTO.CapDanhMuc;
            exists.IsLeaf = dTO.IsLeaf;
            exists.Path = string.IsNullOrWhiteSpace(newParent.Path)
                    ? $"{newParent.IDDanhMuc}/{id}"
                    : $"{newParent.Path}/{id}";
                newParent.IsLeaf = false;
            

            if (oldParentId.HasValue)
            {
                bool oldParentHasOtherChildren = await _context.DanhMucs.AnyAsync(dm =>
                    dm.IDDanhMuc != id && dm.CapDanhMuc!=1 &&
                    dm.Path.EndsWith($"/{oldParentId}") ||
                    dm.Path.Contains($"/{oldParentId}/") ||
                    dm.Path.StartsWith($"{oldParentId}/"));

                var oldParent = await _context.DanhMucs.FirstOrDefaultAsync(x => x.IDDanhMuc == oldParentId.Value);
                if (oldParent != null)
                {
                    oldParent.IsLeaf = !oldParentHasOtherChildren;
                }
            }

            List<HinhAnhDanhMuc> hinhanh = dTO.Images.Select(img => new HinhAnhDanhMuc
            {
                IDDanhMuc = id,
                HinhAnh = img
            }).ToList();

            _context.AddRange(hinhanh);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Message = "Cập nhật danh mục thành công",
                Status = "success"
            };
        }


        public async Task<ResponseDTO> UpdateLvl1(int id, string name)
        {
            var exists = _context.DanhMucs.FirstOrDefault(x => x.IDDanhMuc == id);
            if (exists == null)
            {
                return new ResponseDTO
                {
                    Message = "Không tìm thấy danh mục với ID tương ứng",
                    Status = "error"
                };
            }
            else
            {
                exists.TenDanhMuc = name;
                await _context.SaveChangesAsync();
                return new ResponseDTO
                {
                    Message = "Cập nhật danh mục thành công",
                    Status = "error"
                };
            }
        }

    }
}
