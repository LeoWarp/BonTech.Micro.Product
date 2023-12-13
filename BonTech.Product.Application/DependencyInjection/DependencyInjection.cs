using BonTech.Product.Application.Mapping;
using BonTech.Product.Application.Services;
using BonTech.Product.Application.Validations.FluentValidations;
using BonTech.Product.Domain.Dto;
using BonTech.Product.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BonTech.Product.Application.DependencyInjection
{
    /// <summary>
    /// Внедрение зависимостей слоя Application.
    /// </summary>
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ProductMapping));

            services.ServicesInit();
        }

        private static void ServicesInit(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ProductDto>, CreateValidator>();
            services.AddScoped<IValidator<ProductDto>, UpdateValidator>();

            services.AddScoped<IProductService, ProductService>();
        }
    }
}