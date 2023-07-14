using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using CanWeFixItApi.Services;

namespace CanWeFixItApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class ValuationsController : ControllerBase
    {
        private readonly IValuationsService _iValuationsService;
        private readonly ILogger<ValuationsController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iValuationsService"></param>
        /// <param name="logger"></param>
        public ValuationsController(IValuationsService iValuationsService, ILogger<ValuationsController> logger)
        {
            _iValuationsService = iValuationsService;
            _logger = logger;
        }

        /// <summary>
        /// Return market valuation
        /// </summary>
        /// <returns>A list of market valuation currently with only an item in the list</returns>
        [SwaggerResponse(200, "A list of market valuation if no error")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrument>>> Get()
        {   
            return Ok(await _iValuationsService.GetMarketValuationAsync());
        }
    }
}