using BonTech.Product.Domain.Interfaces;

namespace BonTech.Product.Domain.Entity;

public class Category : IAuditable
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Название категории
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Связь с сущностью продукт
    /// </summary>
    public List<ProductCategory> ProductCategories { get; }

    public DateTime CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}