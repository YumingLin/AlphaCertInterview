using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItApi.Repositories;

namespace CanWeFixItApi.Services
{
    public class InstrumentsService : IInstrumentsService
    {
        private readonly IInstrumentsRepository _iInstrumentsRepository;
        private readonly ILogger<InstrumentsService> _logger;

        public InstrumentsService(IInstrumentsRepository iInstrumentsRepository, ILogger<InstrumentsService> logger)
        {
            _iInstrumentsRepository = iInstrumentsRepository;
            _logger = logger;
        }



        public async Task<IEnumerable<Instrument>> GetInstrumentsAsync()
        {
            _logger.LogInformation("Calling InstrumentsService::GetInstrumentsAsync() ...");
            return await _iInstrumentsRepository.GetInstrumentsAsync();
        }
    }
}
