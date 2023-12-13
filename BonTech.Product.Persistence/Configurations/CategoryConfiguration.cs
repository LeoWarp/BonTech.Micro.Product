using BonTech.Product.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonTech.Product.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(u => u.Name).IsUnique();
            
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(300).IsRequired();

        builder.HasData(new List<Category>()
        {
            new Category()
            {
                Id = 1,
                Name = "Спорт",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            },
            new Category()
            {
                Id = 2,
                Name = "Туризм",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            }
        });
    }
}