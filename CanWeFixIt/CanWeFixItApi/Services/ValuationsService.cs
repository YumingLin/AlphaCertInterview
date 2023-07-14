using CanWeFixItApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ValuationsService : IValuationsService
    {
        private readonly IValuationsRepository _iValuationsRepository;
        private readonly ILogger<ValuationsService> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iValuationsRepository"></param>
        /// <param name="logger"></param>
        public ValuationsService(IValuationsRepository iValuationsRepository, ILogger<ValuationsService> logger)
        {
            _iValuationsRepository = iValuationsRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MarketValuation>> GetMarketValuationAsync()
        {
            _logger.LogInformation("Calling ValuationsService::GetMarketValuationAsync() ...");
            return await _iValuationsRepository.GetMarketValuationAsync();
        }
    }
}
