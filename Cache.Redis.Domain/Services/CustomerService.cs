using System;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Models;
using Cache.Redis.Domain.Repository;
using Cache.Redis.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cache.Redis.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            ICustomerRepository customerRepository,
            ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<bool> StringSetAsync(Customer customer, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringSetAsync)}");

            customer.Key = Guid.NewGuid();

            return await _customerRepository.StringSetAsync(customer, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Customer> StringGetAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(StringGetAsync)}");

            return await _customerRepository.StringGetAsync(key, cancellationToken).ConfigureAwait(false);
        }
    }
}