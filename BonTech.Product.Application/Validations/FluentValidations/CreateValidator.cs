using BonTech.Product.Domain.Dto;
using FluentValidation;

namespace BonTech.Product.Application.Validations.FluentValidations;

public class CreateValidator : AbstractValidator<ProductDto>
{
    public CreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.CategoryIds).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty();
    }
}