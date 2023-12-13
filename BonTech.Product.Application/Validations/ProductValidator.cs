using BonTech.Product.Domain.Entity;
using BonTech.Product.Domain.Enum;
using BonTech.Product.Domain.Interfaces.Validations;
using BonTech.Product.Domain.Result;

namespace BonTech.Product.Application.Validations;

/// <inheritdoc />
public class ProductValidator : IProductValidator
{
    /// <inheritdoc />
    public Result ValidateCreate(Domain.Entity.Product product, IEnumerable<Category> categories)
    {
        if (product != null)
        {
            return new Result()
            {
                ErrorMessage = ErrorMessage.ProductAlreadyExists,
                ErrorCode = (int)ErrorCodes.ProductAlreadyExists
            };
        }
        if (!categories.Any())
        {
            return new Result()
            {
                ErrorMessage = ErrorMessage.CategoryNotFound,
                ErrorCode = (int)ErrorCodes.CategoryNotFound7
            };
        }
        return new Result();
    }

    /// <inheritdoc />
    public Result ValidateDelete(Domain.Entity.Product product)
    {
        if (product == null)
        {
            return new Result()
            {
                ErrorMessage = ErrorMessage.ProductNotFound,
                ErrorCode = (int)ErrorCodes.CategoryNotFound9
            };
        }
        return new Result();
    }

    /// <inheritdoc />
    public Result ValidateUpdate(Domain.Entity.Product product, IEnumerable<Category> categories)
    {
        if (product == null)
        {
            return new Result()
            {
                ErrorMessage = ErrorMessage.ProductNotFound,
                ErrorCode = (int)ErrorCodes.ProductNotFound2
            };
        }
        if (!categories.Any())
        {
            return new Result()
            {
                ErrorMessage = ErrorMessage.CategoryNotFound,
                ErrorCode = (int)ErrorCodes.CategoryNotFound8
            };
        }
        return new Result();
    }
}