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
    public class AddressController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAddressService _addressService;

        public AddressController(
            ILogger<CustomerController> logger,
            IAddressService addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(GetAsync)}");

            try
            {
                var address = await _addressService.StringGetAsync(key, cancellationToken).ConfigureAwait(false);

                if (address == null) return StatusCode((int)HttpStatusCode.NotFound);

                var addressViewModel = new AddressViewModel
                {
                    Street = address.Street,
                    Number = address.Number,
                    District = address.District
                };

                return Ok(addressViewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(AddressViewModel addressViewModel, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(PostAsync)}");

            var address = new Address
            {
                Street = addressViewModel.Street,
                Number = addressViewModel.Number,
                District = addressViewModel.District
            };

            try
            {
                var result = await _addressService.StringSetAsync(address, cancellationToken);

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