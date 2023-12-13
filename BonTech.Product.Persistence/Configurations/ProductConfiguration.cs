using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductEntity = BonTech.Product.Domain.Entity.Product;

namespace BonTech.Product.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(500).IsRequired();
        builder.Property(x => x.Description).HasColumnName("Description").IsRequired();
        builder.Property(x => x.Price).HasColumnName("Price").IsRequired();
        builder.Property(x => x.Quantity).HasColumnName("Quantity").IsRequired();
        
        builder.HasData(new List<ProductEntity>()
        {
            new ProductEntity()
            {
                Id = 1,
                Name = "Спортивные кроссовки",
                Description = "Удобные кроссовки",
                Quantity = 10,
                Price = 3500,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            },
            new ProductEntity()
            {
                Id = 2,
                Name = "Спортивная одежда",
                Description = "Adidas толстовка",
                Quantity = 10,
                Price = 3500,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            }
        });
    }
}