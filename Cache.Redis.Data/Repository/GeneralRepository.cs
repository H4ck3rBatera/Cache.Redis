using System;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Repository;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Cache.Redis.Data.Repository
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly ILogger _logger;
        private readonly IDatabase _database;

        public GeneralRepository(
            ILogger<CustomerRepository> logger,
            IConnectionMultiplexer connectionMultiplexer)
        {
            _logger = logger;
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> KeyDeleteAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(KeyDeleteAsync)}");

            return await _database.KeyDeleteAsync(key.ToString()).ConfigureAwait(false);
        }

        public async Task<TimeSpan?> KeyTimeToLiveAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(KeyTimeToLiveAsync)}");

            return await _database.KeyTimeToLiveAsync(key.ToString()).ConfigureAwait(false);
        }
    }
}