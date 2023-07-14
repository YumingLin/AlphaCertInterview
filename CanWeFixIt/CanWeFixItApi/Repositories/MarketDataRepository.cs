using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Repositories
{
    /// <summary>
    /// An interface for the market data repository between the service and the storage of system data
    /// </summary>
    public class MarketDataRepository : IMarketDataRepository
    {
        private readonly IDatabaseService _iDatabaseService;
        private readonly ILogger<MarketDataRepository> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDatabaseService"></param>
        /// <param name="logger"></param>
        public MarketDataRepository(IDatabaseService iDatabaseService, ILogger<MarketDataRepository> logger)
        {
            _iDatabaseService = iDatabaseService;
            _logger = logger;
        }

        /// <summary>
        /// Get the market valuation in the system
        /// </summary>
        /// <returns>Market valuation</returns>
        public async Task<IEnumerable<MarketDataDto>> GetMarketDataDtoAsync()
        {
            return await _iDatabaseService.MarketDataDto();
        }
    }
}
