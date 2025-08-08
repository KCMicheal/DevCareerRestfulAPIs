using DevCareer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevCareer.Data.Data;

public class DevCareerDbContext : IdentityDbContext<ApplicationUser>
{
    public DevCareerDbContext(DbContextOptions<DevCareerDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = AppRoles.user,
                NormalizedName = AppRoles.user.ToUpper(),
            },
            new IdentityRole
            {
                Id = "2",
                Name = AppRoles.support,
                NormalizedName = AppRoles.support.ToUpper()
            },
            new IdentityRole
            {
                Id = "3",
                Name = AppRoles.admin,
                NormalizedName = AppRoles.admin.ToUpper()
            }
        );
    }

}
