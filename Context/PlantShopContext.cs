using Microsoft.EntityFrameworkCore;
using PlantShop.Models;

namespace PlantShop.Context
{
    public class PlantShopContext : DbContext
    {
        public PlantShopContext(DbContextOptions<PlantShopContext> options) : base(options) 
        {}

        public DbSet<UserModel> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}