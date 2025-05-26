using CategoriesService.Models;
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
        public DbSet<DonViVanChuyen> DonViVanChuyen { get; set; }

        //
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<BienTheSanPham> BienTheSanPham { get; set; }
        public DbSet<ChiTietBienTheSanPham> ChiTietBienTheSanPham { get; set; }
        public DbSet<GiaTriBTSP> GiaTriBTSP {  get; set; }
        public DbSet<ThuocTinhBTSP> ThuocTinhBTSP {  get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<CuaHang> CuaHang {  get; set; }
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
        public DbSet<LogActiveties> LogActiveties { get; set; }
        //
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<CategoriesService.Models.HinhAnhDanhMuc> HinhAnhDanhMucs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DanhMuc>()
                .Property(d => d.IDDanhMuc)
                .ValueGeneratedNever();
            modelBuilder.Entity<CategoriesService.Models.HinhAnhDanhMuc>()
                .Property(h => h.IDHinhAnhDanhMuc)
                .ValueGeneratedOnAdd();
        }
    }
}
