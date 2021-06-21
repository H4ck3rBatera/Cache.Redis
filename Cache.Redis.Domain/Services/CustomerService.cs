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

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(Customer customer, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(RegisterAsync)}");

            return await _customerRepository.RegisterAsync(customer, cancellationToken);
        }
    }
}