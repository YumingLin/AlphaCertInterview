﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItService
{
    public interface IDatabaseService
    {
        Task<IEnumerable<Instrument>> Instruments();
        Task<IEnumerable<MarketDataDto>> MarketDataDto();
        Task<IEnumerable<MarketValuation>> MarketValuation();
        void SetupDatabase();
    }
}