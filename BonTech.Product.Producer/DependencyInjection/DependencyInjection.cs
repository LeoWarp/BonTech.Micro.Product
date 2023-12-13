using BonTech.Product.Producer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BonTech.Product.Producer.DependencyInjection
{
   
    public static class DependencyInjection
    {
        public static void AddProducer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IMessageProducer, ProductProducer>();
        }
    }
}