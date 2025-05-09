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
    }
}
