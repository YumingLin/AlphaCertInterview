using CanWeFixItApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MarketDataService : IMarketDataService
    {
        private readonly IMarketDataRepository _iMarketDataRepository;
        private readonly ILogger<MarketDataService> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMarketDataRepository"></param>
        /// <param name="logger"></param>
        public MarketDataService(IMarketDataRepository iMarketDataRepository, ILogger<MarketDataService> logger)
        {
            _iMarketDataRepository = iMarketDataRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MarketDataDto>> GetMarketDataDtoAsync()
        {
            _logger.LogInformation("Calling MarketDataService::GetMarketDataDtoAsync() ...");
            return await _iMarketDataRepository.GetMarketDataDtoAsync();
        }
    }
}
