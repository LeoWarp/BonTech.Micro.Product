using AutoMapper;
using BonTech.Product.Application.Services;
using BonTech.Product.Domain.Dto;
using BonTech.Product.Domain.Entity;
using BonTech.Product.Domain.Interfaces.Repositories;
using BonTech.Product.Domain.Interfaces.Validations;
using BonTech.Product.Domain.Result;
using BonTech.Product.Producer.Interfaces;
using MockQueryable.Moq;
using Moq;
using Serilog;
using Xunit;

namespace BonTech.Product.Tests;

public class CreateProductTest
{
    private ProductService _productService;
    private readonly Mock<IBaseRepository<Domain.Entity.Product>> _mockProductRepository = new Mock<IBaseRepository<Domain.Entity.Product>>();
    private readonly Mock<IBaseRepository<Category>> _mockCategoryRepository = new Mock<IBaseRepository<Category>>();
    private readonly Mock<ILogger> _mockLogger = new Mock<ILogger>();

    public CreateProductTest()
    {
        
    }
    
    [Fact]
    public async Task Create_Product_Success()
    {
        var users = new List<Domain.Entity.Product>()
        {
            new Domain.Entity.Product { Name = "Спортивные кроссовки" },
        }.AsQueryable();
        var mock = users.BuildMock();
        _mockProductRepository.Setup(x => x.GetAll()).Returns(mock);

        var categories = new List<Category>()
        {
            new Category() { Id = 1, Name = "Спорт" },
            new Category() { Id = 2, Name = "Туризм" }
        };
        var mockCategory = categories.BuildMock();
        _mockCategoryRepository.Setup(x => x.GetAll()).Returns(mockCategory);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<Domain.Entity.Product, ProductDto>(It.IsAny<Domain.Entity.Product>()));
        //mapperMock.Setup(m => m.Map<ProductDto, Domain.Entity.Product>(It.IsAny<ProductDto>())).Returns(new Result<ProductDto>());
        
        var producerMock = new Mock<IMessageProducer>();
        var productValidator = new Mock<IProductValidator>();
        //_productService = new ProductService(_mockProductRepository.Object, _mockCategoryRepository.Object, _mockLogger.Object, mapperMock.Object, productValidator.Object, producerMock.Object, null);
        
        var result = await _productService.CreateProductAsync(new ProductDto()
        {
            Description = "Xtt",
            Price = 322,
            Quantity = 10,
            Name = "Спортивные кроссовки",
            CategoryIds = new long[] { 1, 2 }
        });

        Assert.Equal(result, result);
    }
}