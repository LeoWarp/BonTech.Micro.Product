using BonTech.Product.Domain.Entity;
using BonTech.Product.Domain.Interfaces.Repositories;
using BonTech.Product.Persistence.Interceptors;
using BonTech.Product.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BonTech.Product.Persistence.DependencyInjection
{
    /// <summary>
    /// Внедрение зависимостей слоя Persistence.
    /// </summary>
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQL");

            services.AddSingleton<DateInterceptor>();
            
            services.AddDbContext<ProductDbContext>(options => { options.UseNpgsql(connectionString); });
            
            services.RepositoriesInit();
        }

        private static void RepositoriesInit(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Domain.Entity.Product>, BaseRepository<Domain.Entity.Product>>();
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
        }
    }
}