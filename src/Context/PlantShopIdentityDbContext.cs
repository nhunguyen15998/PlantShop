using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantShop.Models;

namespace PlantShop.Context;

public class PlantShopIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public PlantShopIdentityDbContext(DbContextOptions<PlantShopIdentityDbContext> options)
        : base(options)
    {}

    public DbSet<UserModel> User { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
