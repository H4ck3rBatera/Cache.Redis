using Cache.Redis.Data.Repository;
using Cache.Redis.Data.Support.Options;
using Cache.Redis.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Cache.Redis.Data.Support.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<RedisOption>(configuration.GetSection("Redis"));

            var connectionString = configuration.GetSection("ConnectionStrings:Redis");
            services.AddSingleton<IConnectionMultiplexer>(serviceProvider => ConnectionMultiplexer.Connect(connectionString.Value));

            services
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IAddressRepository, AddressRepository>()
                .AddScoped<IGeneralRepository, GeneralRepository>();

            return services;
        }
    }
}