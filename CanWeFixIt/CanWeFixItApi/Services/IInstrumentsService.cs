using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Services
{
    /// <summary>
    /// An interface for the intruments service which acts as middleman between the controller and the repository with all business logic 
    /// </summary>
    public interface IInstrumentsService
    {
        /// <summary>
        /// Return all instruments from the database that are currently `active`
        /// </summary>
        /// <returns>A list of all the acitve instruments</returns>
        Task<IEnumerable<Instrument>> GetInstrumentsAsync();
    }
}
