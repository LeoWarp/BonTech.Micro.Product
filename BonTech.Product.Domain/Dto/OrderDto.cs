namespace BonTech.Product.Domain.Dto;

/// <summary>
/// Модель для получения продуктов списком названий с последующим оформлением заказа 
/// </summary>
public class OrderDto
{
    public List<string> Products { get; set; }
}