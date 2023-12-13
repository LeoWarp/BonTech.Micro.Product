using BonTech.Product.Domain.Dto;
using BonTech.Product.Domain.Result;

namespace BonTech.Product.Domain.Interfaces.Services;
/// <summary>
/// Сервис отвечающий за работу с доменной части Product
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Получение всех продуктов
    /// </summary>
    Task<CollectionResult<ProductDto>> GetProductsAsync();
    
    /// <summary>
    /// Получение продукта по идентификатору
    /// </summary>
    /// <param name="id"></param>
    Task<Result<ProductDto>> GetProductAsync(long id);
    
    /// <summary>
    /// Создание продукта с базовыми параметрами
    /// </summary>
    /// <param name="dto"></param>
    Task<Result<ProductDto>> CreateProductAsync(ProductDto dto);
    
    /// <summary>
    /// Удаление продукта по идентификатору
    /// </summary>
    /// <param name="id"></param>
    Task<Result<Entity.Product>> DeleteProductAsync(long id);
    
    /// <summary>
    /// Обновление параметров продукта по идентификатору 
    /// </summary>
    /// <param name="dto"></param>
    Task<Result<ProductDto>> UpdateProductAsync(ProductDto dto);
}