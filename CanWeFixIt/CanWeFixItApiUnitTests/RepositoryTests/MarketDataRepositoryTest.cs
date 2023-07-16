using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CanWeFixItApi.Services;
using CanWeFixItApi.Repositories;
using CanWeFixItService;

namespace CanWeFixItApiUnitTests.RepositoryTests
{
    public class MarketDataRepositoryTest
    {
        Mock<ILogger<MarketDataRepository>> _mockLogger;
        Mock<IDatabaseService> _mockDatabaseService;
        MarketDataRepository _sut;

        public MarketDataRepositoryTest()
        {
            _mockLogger = new Mock<ILogger<MarketDataRepository>>();
            _mockDatabaseService = new Mock<IDatabaseService>();
            _sut = new MarketDataRepository(_mockDatabaseService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetMarketData_Should_Call_Repository_Once()
        {
            _mockDatabaseService.Setup(x => x.MarketDataDto());
            var result = await _sut.GetMarketDataDtoAsync();
            _mockDatabaseService.Verify(x=>x.MarketDataDto(), Times.Once);
        }

      
        [Fact]
        public async Task GetMarketData_Should_Return_Correct_Result()
        {
            const int id1 = 1, id2 = 2;
            const int dataValue1 = 100, dataValue2 = 200;
            const int instrumentId1 = 1000, instrumentId2 = 2000;
            var item1 = new MarketDataDto() { Id = id1, Active = true, DataValue = dataValue1, InstrumentId = instrumentId1 };
            var item2 = new MarketDataDto() { Id = id2, Active = true, DataValue = dataValue2, InstrumentId = instrumentId2 };
            _mockDatabaseService.Setup(x => x.MarketDataDto()).ReturnsAsync(new[] {item1, item2} );
            var result = await _sut.GetMarketDataDtoAsync();
            var list = Assert.IsType<MarketDataDto[]>(result);
            Assert.Equal(2, list.Count());
            Assert.Equal(id1, list[0].Id);
            Assert.Equal(dataValue1, list[0].DataValue);
            Assert.Equal(instrumentId1, list[0].InstrumentId);
            Assert.Equal(2, list[1].Id);
            Assert.Equal(dataValue2, list[1].DataValue);
            Assert.Equal(instrumentId2, list[1].InstrumentId);


        }

    }
}
