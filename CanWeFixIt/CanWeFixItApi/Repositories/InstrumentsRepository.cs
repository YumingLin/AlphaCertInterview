using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CanWeFixItApi.Repositories
{
    public class InstrumentsRepository : IInstrumentsRepository
    {
        private readonly IDatabaseService _iDatabaseService;
        private readonly ILogger<InstrumentsRepository> _logger;

        public InstrumentsRepository(IDatabaseService iDatabaseService, ILogger<InstrumentsRepository> logger)
        {
            _iDatabaseService = iDatabaseService;
            _logger = logger;
        }

        /// <summary>
        /// Return all instruments from the database that are currently `active`
        /// </summary>
        /// <returns>A list of all the acitve instruments</returns>
        public async Task<IEnumerable<Instrument>> GetInstrumentsAsync()
        {
            return await _iDatabaseService.Instruments();
        }
    }
}
