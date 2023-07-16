using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CanWeFixItApi.Services;
using CanWeFixItApi.Repositories;
using CanWeFixItService;

namespace CanWeFixItApiUnitTests.RepositoryTests
{
    public class ValuationsRepositoryTest
    {
        Mock<ILogger<ValuationsRepository>> _mockLogger;
        Mock<IDatabaseService> _mockDatabaseService;
        ValuationsRepository _sut;

        public ValuationsRepositoryTest()
        {
            _mockLogger = new Mock<ILogger<ValuationsRepository>>();
            _mockDatabaseService = new Mock<IDatabaseService>();
            _sut = new ValuationsRepository(_mockDatabaseService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetMarketValuation_Should_Call_Repository_Once()
        {
            _mockDatabaseService.Setup(x => x.MarketValuation());
            var result = await _sut.GetMarketValuationAsync();
            _mockDatabaseService.Verify(x=>x.MarketValuation(), Times.Once);
        }
      
        [Fact]
        public async Task GetMarketValuation_Should_Return_Correct_Result()
        {
            const long total = 1000;
            const string name = "test market name";
            var item1 = new MarketValuation() { Name = name, Total = total };
            _mockDatabaseService.Setup(x => x.MarketValuation()).ReturnsAsync(new[] {item1} );
            var result = await _sut.GetMarketValuationAsync();
            var list = Assert.IsType<MarketValuation[]>(result);
            Assert.Single(list);
            Assert.Equal(name, list[0].Name);
            Assert.Equal(total, list[0].Total);
        }
    }
}
