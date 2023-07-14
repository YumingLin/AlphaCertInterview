using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Services
{
    /// <summary>
    /// An interface for the market data service which acts as middleman between the controller and the repository with all business logic 
    /// </summary>
    public interface IMarketDataService
    {
        /// <summary>
        /// Return all market data from the database that are currently `active`
        /// </summary>
        /// <returns>A list of all the acitve narket data</returns>
        Task<IEnumerable<MarketDataDto>> GetMarketDataDtoAsync();
    }
}
