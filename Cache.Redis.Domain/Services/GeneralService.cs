using System;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Repository;
using Cache.Redis.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cache.Redis.Domain.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly ILogger _logger;
        private readonly IGeneralRepository _generalRepository;

        public GeneralService(
            IGeneralRepository generalRepository,
            ILogger<CustomerService> logger)
        {
            _generalRepository = generalRepository;
            _logger = logger;
        }
        
        public async Task<bool> KeyDeleteAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(KeyDeleteAsync)}");

            return await _generalRepository.KeyDeleteAsync(key, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TimeSpan?> KeyTimeToLiveAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(KeyTimeToLiveAsync)}");

            return await _generalRepository.KeyTimeToLiveAsync(key, cancellationToken).ConfigureAwait(false);
        }
    }
}