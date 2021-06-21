using Cache.Redis.Domain.Services;
using Cache.Redis.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Cache.Redis.Domain.Support.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}