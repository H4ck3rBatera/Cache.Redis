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
    public class StringsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICustomerService _customerService;

        public StringsController(
            ILogger<StringsController> logger,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(Get)}");

            try
            {
                var customer = await _customerService.StringGetAsync(key, cancellationToken).ConfigureAwait(false);

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
        public async Task<IActionResult> Post(CustomerViewModel customerViewModel, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(Post)}");

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

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(Delete)}");

            try
            {
                var result = await _customerService.KeyDeleteAsync(key, cancellationToken);

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