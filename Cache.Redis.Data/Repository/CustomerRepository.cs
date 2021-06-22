using System;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Data.Support.Options;
using Cache.Redis.Domain.Models;
using Cache.Redis.Domain.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Cache.Redis.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger _logger;
        private readonly IDatabase _database;
        private readonly RedisOption _redisOption;

        public CustomerRepository(
            ILogger<CustomerRepository> logger,
            IConnectionMultiplexer connectionMultiplexer,
            IOptions<RedisOption> redisOption)
        {
            _logger = logger;
            _redisOption = redisOption.Value;
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> StringSetAsync(Customer customer, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringSetAsync)}");

            return await _database
                .StringSetAsync(customer.Key.ToString(), customer.Name, TimeSpan.FromSeconds(_redisOption.Expiry))
                .ConfigureAwait(false);
        }

        public async Task<Customer> StringGetAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringGetAsync)}");

            var redisValue = await _database.StringGetAsync(key.ToString()).ConfigureAwait(false);

            return redisValue.HasValue ? new Customer { Name = redisValue } : null;
        }
    }
}