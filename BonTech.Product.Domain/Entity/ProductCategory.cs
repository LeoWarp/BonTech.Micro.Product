namespace BonTech.Product.Domain.Entity;

public class ProductCategory
{
    public long ProductId { get; set; }
    public long CategoryId { get; set; }
    public Product Product { get; set; } 
    public Category Category { get; set; } 
}