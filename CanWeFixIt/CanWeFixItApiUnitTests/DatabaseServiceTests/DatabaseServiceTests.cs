using CanWeFixItApi.Repositories;
using CanWeFixItService;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanWeFixItApiUnitTests.DatabaseServiceTests
{
    /// <summary>
    /// This test class replicates the test criteria as implemented in the YesWeCan console app.
    /// </summary>
    public class DatabaseServiceTests
    {
        static DatabaseService? _sut;

        public DatabaseServiceTests()
        {
            if (_sut != null) return;
            _sut = new DatabaseService();
            _sut.SetupDatabase();
        }

        [Fact]
        public async Task MarketData_Verification()
        {
            var list = _sut==null? new List<MarketDataDto>():await _sut.MarketDataDto();
            Assert.Equal(2, list.Count());
            Assert.True(list.All(x => x.Active));
            Assert.True(list.All(x => x.Id is 2 or 4));
            Assert.True(list.All(x => x.InstrumentId is 2 or 4));
        }

        [Fact]
        public async Task Instrument_Verification()
        {
            var list = _sut == null ? new List<Instrument>():await _sut.Instruments();
            Assert.Equal(4, list.Count());
            Assert.True(list.All(x => x.Active));
            Assert.True(list.All(x => x.Id is 2 or 4 or 6 or 8));
        }

        [Fact]
        public async Task MarketValuation_Verification()
        {
            var list = _sut==null? new List<MarketValuation>(): await _sut.MarketValuation();
            Assert.Single(list);
            Assert.Equal("DataValueTotal", list.First().Name);
            Assert.Equal(13332, list.First().Total);
        }


    }
}
