using BonTech.Product.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonTech.Product.Persistence.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(sc => new { sc.ProductId, sc.CategoryId });
        
        builder
            .HasOne(sc => sc.Category)
            .WithMany(s => s.ProductCategories)
            .HasForeignKey(sc => sc.CategoryId);
        
        builder
            .HasOne(sc => sc.Product)
            .WithMany(s => s.ProductCategories)
            .HasForeignKey(sc => sc.ProductId);
        
        
        builder.HasData(new List<ProductCategory>()
        {
            new ProductCategory()
            {
                CategoryId = 1,
                ProductId = 1
            }
        });
    }
}