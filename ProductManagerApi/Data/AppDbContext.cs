using Microsoft.EntityFrameworkCore;
using ProductManagerApi.Models;

namespace ProductManagerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
