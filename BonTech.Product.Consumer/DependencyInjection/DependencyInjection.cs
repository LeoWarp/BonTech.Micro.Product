using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BonTech.Product.Consumer.DependencyInjection
{
    /// <summary>
    /// Внедрение зависимостей слоя Persistence.
    /// </summary>
    public static class DependencyInjection
    {
        public static void AddConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddHostedService<RabbitMqListener>();
        }
    }
}