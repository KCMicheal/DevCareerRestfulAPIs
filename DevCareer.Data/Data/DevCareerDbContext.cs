using DevCareer.Data.Models;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace DevCareer.Data.Data;

public class DevCareerDbContext : DbContext
{
    public DevCareerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Tag> Tags { get; set; }
}
