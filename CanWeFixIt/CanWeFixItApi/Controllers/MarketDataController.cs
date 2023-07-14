using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using CanWeFixItApi.Services;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class MarketDataController : ControllerBase
    {
        private readonly IMarketDataService _iMarketDataService;
        private readonly ILogger<MarketDataController> _logger;

        public MarketDataController(IMarketDataService iMarketDataService, ILogger<MarketDataController> logger)
        {
            _iMarketDataService = iMarketDataService;
            _logger = logger;
        }

        /// <summary>
        /// Return all active market data from the system
        /// </summary>
        /// <returns>active market data from the system</returns>
        [SwaggerResponse(200, "A list of all active market data if no error")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> Get()
        {
            return Ok(await _iMarketDataService.GetMarketDataDtoAsync());
        }

        
    }
}