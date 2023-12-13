namespace BonTech.Product.Domain.Dto;

/// <summary>
/// Модель предназначенная для добавления и обновления сущности Product
/// </summary>
public class ProductDto
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
    /// Идентификатор категории продукта
    /// </summary>
    public IEnumerable<long> CategoryIds { get; set; }
}