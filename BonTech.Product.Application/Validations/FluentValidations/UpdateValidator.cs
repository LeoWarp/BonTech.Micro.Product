using BonTech.Product.Domain.Dto;
using FluentValidation;

namespace BonTech.Product.Application.Validations.FluentValidations;

public class UpdateValidator : AbstractValidator<ProductDto>
{
    public UpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Price).NotEmpty().InclusiveBetween(0, 50000);
        RuleFor(x => x.CategoryIds).NotEmpty();
    }
}