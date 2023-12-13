using AutoMapper;
using BonTech.Product.Domain.Dto;
using BonTech.Product.Domain.Entity;
using BonTech.Product.Domain.Enum;
using BonTech.Product.Domain.Interfaces.Repositories;
using BonTech.Product.Domain.Interfaces.Services;
using BonTech.Product.Domain.Interfaces.Validations;
using BonTech.Product.Domain.Result;
using BonTech.Product.Domain.Settings;
using BonTech.Product.Producer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using ProductEntity = BonTech.Product.Domain.Entity.Product;

namespace BonTech.Product.Application.Services;

/// <inheritdoc />
public class ProductService : IProductService
{
    private readonly IBaseRepository<ProductEntity> _productRepository;
    private readonly IBaseRepository<Category> _categoryRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IProductValidator _productValidator;
    //private readonly IMessageProducer _messageProducer;
    private readonly IOptions<RabbitMqParams> _options;

    public ProductService(IBaseRepository<ProductEntity> productRepository,
        IBaseRepository<Category> categoryRepository,
        ILogger logger, IMapper mapper, IProductValidator productValidator,
        //IMessageProducer messageProducer,
        IOptions<RabbitMqParams> options)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _logger = logger.ForContext<ProductService>();
        _mapper = mapper;
        _productValidator = productValidator;
        //_messageProducer = messageProducer;
        _options = options;
    }

    /// <inheritdoc />
    public async Task<CollectionResult<ProductDto>> GetProductsAsync()
    {
        ProductDto[] products;
        try
        {
            products = await _productRepository.GetAll()
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    CategoryIds = x.ProductCategories.Select(y => y.CategoryId)
                })
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, ex.Message);
            return new CollectionResult<ProductDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }

        if (!products.Any())
        {
            _logger.Warning(ErrorMessage.ProductsNotFound, products.Length);
            return new CollectionResult<ProductDto>()
            {
                ErrorMessage = ErrorMessage.ProductsNotFound,
                ErrorCode = (int)ErrorCodes.ProductsNotFound
            };
        }
        return new CollectionResult<ProductDto>()
        {
            Data = products,
            Count = products.Length
        };
    }

    /// <inheritdoc />
    public async Task<Result<ProductDto>> GetProductAsync(long id)
    {
        ProductDto product;
        try
        {
            product = await _productRepository.GetAll()
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    CategoryIds = x.ProductCategories.Select(y => y.CategoryId)
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, ex.Message);
            return new Result<ProductDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
        if (product == null)
        {
            _logger.Warning("Продукт с {Id} не найден", id);
            return new Result<ProductDto>()
            {
                ErrorMessage = ErrorMessage.ProductNotFound,
                ErrorCode = (int)ErrorCodes.ProductNotFound1
            };
        }
        return new Result<ProductDto>()
        {
            Data = product
        };
    }

    /// <inheritdoc />
    public async Task<Result<ProductDto>> CreateProductAsync(ProductDto dto)
    {
        try
        {
            var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
            var categories = await _categoryRepository.GetAll()
                .Where(x => dto.CategoryIds.Contains(x.Id))
                .ToListAsync();
            var result = _productValidator.ValidateCreate(product, categories);
            if (!result.IsSuccess)
            {
                return new Result<ProductDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }
            product = new ProductEntity()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Quantity = dto.Quantity,
                ProductCategories = categories.Select(x => new ProductCategory() { CategoryId = x.Id}).ToList()
            };
            await _productRepository.CreateAsync(product);
            
            //_messageProducer.SendMessage(product, _options.Value.RoutingKey, _options.Value.ExchangeName);
            
            return new Result<ProductDto>()
            {
                Data = _mapper.Map<ProductDto>(product)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new Result<ProductDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    /// <inheritdoc />
    public async Task<Result<ProductEntity>> DeleteProductAsync(long id)
    {
        try
        {
            var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            var result = _productValidator.ValidateDelete(product);
            if (!result.IsSuccess)
            {
                return new Result<ProductEntity>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }
            await _productRepository.RemoveAsync(product);
            return new Result<ProductEntity>()
            {
                Data = product
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new Result<ProductEntity>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    /// <inheritdoc />
    public async Task<Result<ProductDto>> UpdateProductAsync(ProductDto dto)
    {
        try
        {
            var product = await _productRepository.GetAll()
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            var categories = await _categoryRepository.GetAll().Where(x => dto.CategoryIds.Contains(x.Id)).ToListAsync();
            var result = _productValidator.ValidateUpdate(product,  categories);
            if (!result.IsSuccess)
            {
                return new Result<ProductDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }
            product.Description = dto.Description;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.ProductCategories = categories.Select(x => new ProductCategory() { CategoryId = x.Id }).ToList();
            product.Quantity = dto.Quantity;

            await _productRepository.UpdateAsync(product);
            var mapProductResult = _mapper.Map<ProductDto>(product);
            return new Result<ProductDto>()
            {
                Data = mapProductResult
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new Result<ProductDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }
}