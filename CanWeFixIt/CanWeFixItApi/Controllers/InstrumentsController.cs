using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CanWeFixItApi.Services;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class InstrumentsController : ControllerBase
    {
        private readonly IInstrumentsService _iInstrumentsService;
        private readonly ILogger<InstrumentsController> _logger;

        public InstrumentsController(IInstrumentsService iInstrumentsService, ILogger<InstrumentsController> logger)
        {
            _iInstrumentsService = iInstrumentsService;
            _logger = logger;
        }

        /// <summary>
        /// Return all active instruments from the system
        /// </summary>
        /// <returns>active instruments from the system</returns>
        [SwaggerResponse(200, "A list of all active instrucments if no error")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrument>>> Get()
        {   
            return Ok(await _iInstrumentsService.GetInstrumentsAsync());
        }
    }
}