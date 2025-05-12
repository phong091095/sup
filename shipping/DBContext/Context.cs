using Microsoft.EntityFrameworkCore;
using shipping.Model;
namespace shipping.DBContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<ChiTietDVVanChuyen> ChiTietDVVanChuyen { get; set; }
        public DbSet<DonViVanChuyen> DonViVanChuyen { get;set; }
        public DbSet<DanhMuc> DanhMuc { get; set; }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<BienTheSanPham> BienTheSanPham { get; set; }
        public DbSet<ChiTietBienTheSanPham> ChiTietBienTheSanPham { get; set; }
        public DbSet<GiaTriBTSP> GiaTriBTSP {  get; set; }
        public DbSet<ThuocTinhBTSP> ThuocTinhBTSP {  get; set; }
        public DbSet<Images> Images { get; set; }
    }
}
