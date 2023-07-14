using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Services
{
    /// <summary>
    /// An interface for the market valuation service which acts as middleman between the controller and the repository with all business logic 
    /// </summary>
    public interface IValuationsService
    {
        /// <summary>
        /// Return market valuation from the database that are currently `active`
        /// </summary>
        /// <returns>market valuation</returns>
        Task<IEnumerable<MarketValuation>> GetMarketValuationAsync();
    }
}
