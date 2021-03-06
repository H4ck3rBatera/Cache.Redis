using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Api.ViewModels;
using Cache.Redis.Domain.Models;
using Cache.Redis.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cache.Redis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(
            ILogger<CustomerController> logger,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(GetAsync)}");

            try
            {
                var customer = await _customerService.StringGetAsync(key, cancellationToken).ConfigureAwait(false);

                if (customer == null) return StatusCode((int)HttpStatusCode.NotFound);

                var customerViewModel = new CustomerViewModel { Name = customer.Name };
                return Ok(customerViewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync(CustomerViewModel customerViewModel, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(PostAsync)}");

            var customer = new Customer { Name = customerViewModel.Name };

            try
            {
                var result = await _customerService.StringSetAsync(customer, cancellationToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}