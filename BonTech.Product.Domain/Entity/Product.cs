using BonTech.Product.Domain.Interfaces;

namespace BonTech.Product.Domain.Entity;

public class Product : IAuditable
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Название продукта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание продукта
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Стоимость продукта
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Количество продукта
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Категории продукта
    /// </summary>
    public List<ProductCategory> ProductCategories { get; set; }
    
    /// <summary>
    /// Внешний ключ для связи с продуктом
    /// </summary>

    public DateTime CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}