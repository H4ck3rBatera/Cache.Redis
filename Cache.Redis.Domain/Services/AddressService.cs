using System;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Models;
using Cache.Redis.Domain.Repository;
using Cache.Redis.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cache.Redis.Domain.Services
{
    public class AddressService : IAddressService
    {
        private readonly ILogger _logger;
        private readonly IAddressRepository _addressRepository;

        public AddressService(
            IAddressRepository addressRepository,
            ILogger<CustomerService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<bool> StringSetAsync(Address address, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringSetAsync)}");

            address.Key = Guid.NewGuid();

            return await _addressRepository.StringSetAsync(address, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Address> StringGetAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringGetAsync)}");

            return await _addressRepository.StringGetAsync(key, cancellationToken).ConfigureAwait(false);
        }
    }
}