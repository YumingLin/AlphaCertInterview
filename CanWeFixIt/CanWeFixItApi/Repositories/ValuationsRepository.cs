using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Repositories
{
    /// <summary>
    /// An interface for the market valuation repository between the service and the storage of system data
    /// </summary>
    public class ValuationsRepository : IValuationsRepository
    {
        private readonly IDatabaseService _iDatabaseService;
        private readonly ILogger<ValuationsRepository> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iDatabaseService"></param>
        /// <param name="logger"></param>
        public ValuationsRepository(IDatabaseService iDatabaseService, ILogger<ValuationsRepository> logger)
        {
            _iDatabaseService = iDatabaseService;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MarketDataDto>> GetMarketDataDtoAsync()
        {
            return await _iDatabaseService.MarketDataDto();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<IEnumerable<MarketValuation>> GetMarketValuationAsync()
        {
            return await _iDatabaseService.MarketValuation();
        }
    }
}
