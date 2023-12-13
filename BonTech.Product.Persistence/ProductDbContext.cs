using System.Reflection;
using BonTech.Product.Domain.Entity;
using BonTech.Product.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace BonTech.Product.Persistence;

public sealed class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    public  DbSet<Domain.Entity.Product> Products { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new DateInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}