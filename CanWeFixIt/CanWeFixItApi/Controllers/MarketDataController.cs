using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using CanWeFixItApi.Services;
using System.Net;
using System;

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
            try
            {
                var result = await _iMarketDataService.GetMarketDataDtoAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                //We should use middleware for exception handling and return friendly message system wide
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        
    }
}