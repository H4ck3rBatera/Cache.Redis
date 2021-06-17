using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Cache.Redis.Data.Support.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings:Redis:ConnectionString");
            services.AddSingleton(serviceProvider => ConnectionMultiplexer.Connect(connectionString.Value));

            return services;
        }
    }
}