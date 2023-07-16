using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CanWeFixItApi.Services;
using CanWeFixItApi.Repositories;
using CanWeFixItService;

namespace CanWeFixItApiUnitTests.ServiceTests
{
    public class ValuationsServiceTest
    {
        Mock<ILogger<ValuationsService>> _mockLogger;
        Mock<IValuationsRepository> _mockRepository;
        ValuationsService _sut;

        public ValuationsServiceTest()
        {
            _mockLogger = new Mock<ILogger<ValuationsService>>();
            _mockRepository = new Mock<IValuationsRepository>();
            _sut = new ValuationsService(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetMarketValuation_Should_Call_Repository_Once()
        {
            _mockRepository.Setup(x => x.GetMarketValuationAsync());
            var result = await _sut.GetMarketValuationAsync();
            _mockRepository.Verify(x=>x.GetMarketValuationAsync(), Times.Once);
        }

      
        [Fact]
        public async Task GetMarketValuation_Should_Return_Correct_Result()
        {
            const long total = 1000;
            const string name = "test market name";
            var item1 = new MarketValuation() { Name = name, Total = total };
            _mockRepository.Setup(x => x.GetMarketValuationAsync()).ReturnsAsync(new[] {item1} );
            var result = await _sut.GetMarketValuationAsync();
            var list = Assert.IsType<MarketValuation[]>(result);
            Assert.Single(list);
            Assert.Equal(name, list[0].Name);
            Assert.Equal(total, list[0].Total);
        }

    }
}
