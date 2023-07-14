using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Repositories
{
    /// <summary>
    /// An interface for the instrumens repository between the service and the storage of system data
    /// </summary>
    public interface IInstrumentsRepository
    {
        /// <summary>
        /// Return all instruments from the database that are currently `active`
        /// </summary>
        /// <returns>A list of all the acitve instruments</returns>
        Task<IEnumerable<Instrument>> GetInstrumentsAsync();
    }
}
