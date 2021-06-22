using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Cache.Redis.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cache.Redis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IGeneralService _generalService;

        public GeneralController(
            ILogger<GeneralController> logger,
            IGeneralService generalService)
        {
            _logger = logger;
            _generalService = generalService;
        }
        
        [HttpGet("{key}/KeyTimeToLive")]
        public async Task<IActionResult> GetKeyTimeToLiveAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(GetKeyTimeToLiveAsync)}");

            try
            {
                var timeSpan = await _generalService.KeyTimeToLiveAsync(key, cancellationToken).ConfigureAwait(false);

                return Ok(timeSpan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteAsync(Guid key, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entering {nameof(DeleteAsync)}");

            try
            {
                var result = await _generalService.KeyDeleteAsync(key, cancellationToken);

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