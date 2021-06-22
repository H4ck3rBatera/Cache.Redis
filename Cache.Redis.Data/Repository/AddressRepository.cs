using System;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Data.Support.Options;
using Cache.Redis.Domain.Models;
using Cache.Redis.Domain.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Cache.Redis.Data.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ILogger _logger;
        private readonly IDatabase _database;
        private readonly RedisOption _redisOption;

        public AddressRepository(
            ILogger<CustomerRepository> logger,
            IConnectionMultiplexer connectionMultiplexer,
            IOptions<RedisOption> redisOption)
        {
            _logger = logger;
            _redisOption = redisOption.Value;
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> StringSetAsync(Address address, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringSetAsync)}");

            var json = JsonConvert.SerializeObject(address);

            return await _database
                .StringSetAsync(address.Key.ToString(), json, TimeSpan.FromSeconds(_redisOption.Expiry))
                .ConfigureAwait(false);
        }

        public async Task<Address> StringGetAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringGetAsync)}");

            var redisValue = await _database.StringGetAsync(key.ToString()).ConfigureAwait(false);

            return redisValue.HasValue ? JsonConvert.DeserializeObject<Address>(redisValue) : null;
        }
    }
}