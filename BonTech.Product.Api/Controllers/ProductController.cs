using BonTech.Product.Domain.Dto;
using BonTech.Product.Domain.Interfaces.Services;
using BonTech.Product.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BonTech.Product.Api.Controllers;
 
//[Authorize]
[AllowAnonymous]
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    /// <summary>
    /// Получение списка всех продуктов из БД
    /// </summary>
    [HttpGet("products")]
    public async Task<ActionResult<CollectionResult<ProductDto>>> GetProducts()
    {
        var response = await _productService.GetProductsAsync();
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response.ErrorCode);
    }
    
    /// <summary>
    /// Получение продукта по Id
    /// </summary>
    /// <param name="id"></param>ProductDtoProductDtoProductDtoProductDto
    /// <response code="200">Возвращается полученный продукт</response>
    /// <response code="400">Если продукт был не найден</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ProductDto>>> GetProduct(long id)
    {
        var response = await _productService.GetProductAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response.Data);
        }
        return BadRequest(response);
    }
    
    /// <summary>
    /// Создание продукта с указанием основных свойств и категорий
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Todo
    ///     {
    ///        "name": "Item #1",
    ///        "description": "Test description",
    ///        "price": 200,
    ///        "quantity": 1,
    ///        "categoryId": integer
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Возвращается полученный продукт</response>
    /// <response code="400">Если продукт был не найден</response>
    [HttpPost]
    public async Task<ActionResult<Result<ProductDto>>> CreateProduct([FromBody] ProductDto dto)
    {
        var response = await _productService.CreateProductAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    /// <summary>
    /// Обновление продукта по Id
    /// </summary>
    /// <param name="dto"></param>
    [HttpPut]
    public async Task<ActionResult<Result<ProductDto>>> UpdateProduct([FromBody] ProductDto dto)
    {
        var response = await _productService.UpdateProductAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    /// <summary>
    /// Удаление продукта по Id
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<Domain.Entity.Product>>> DeleteProduct(long id)
    {
        var response = await _productService.DeleteProductAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response.ErrorCode);
    }
}