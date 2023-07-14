using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Repositories
{
    /// <summary>
    /// An interface for the market valuation repository between the service and the storage of system data
    /// </summary>
    public interface IValuationsRepository
    {
        /// <summary>
        /// Return the sum of of all currently `active` `MarketData`.
        /// </summary>
        /// <returns>The sum of of all currently `active` `MarketData`.</returns>
        Task<IEnumerable<MarketValuation>> GetMarketValuationAsync();
    }
}
