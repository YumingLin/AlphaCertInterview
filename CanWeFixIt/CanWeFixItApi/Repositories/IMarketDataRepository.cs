using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItService;
namespace CanWeFixItApi.Repositories
{
    /// <summary>
    /// An interface for the market data repository between the service and the storage of system data
    /// </summary>
    public interface IMarketDataRepository
    {
        /// <summary>
        /// Return all market data from the database that are currently `active`
        /// </summary>
        /// <returns>A list of all the acitve market data</returns>
        Task<IEnumerable<MarketDataDto>> GetMarketDataDtoAsync();
    }
}
