namespace BonTech.Product.Domain.Dto;

/// <summary>
/// Модель предназначенная для оформления заказа
/// </summary>
public class OrderProductDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }

    public long CategoryId { get; set; }
}