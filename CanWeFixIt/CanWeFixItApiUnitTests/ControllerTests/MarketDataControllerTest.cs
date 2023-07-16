using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CanWeFixItApi.Controllers;
using CanWeFixItApi.Services;
using CanWeFixItService;

namespace CanWeFixItApiUnitTests.ControllerTests
{
    public class MarketDataControllerTest
    {
        Mock<ILogger<MarketDataController>> _mockLogger;
        Mock<IMarketDataService> _mockService;
        MarketDataController _sut;

        public MarketDataControllerTest()
        {
            _mockLogger = new Mock<ILogger<MarketDataController>>();
            _mockService = new Mock<IMarketDataService>();
            _sut = new MarketDataController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetMarketData_Should_Call_Service_Once()
        {
            _mockService.Setup(x => x.GetMarketDataDtoAsync());
            var result = await _sut.Get();
            _mockService.Verify(x=>x.GetMarketDataDtoAsync(), Times.Once);
        }

      
        [Fact]
        public async Task GetMarketData_Should_Return_Correct_Result()
        {
            const int id1 = 1, id2 = 2;
            const int dataValue1 = 100, dataValue2 = 200;
            const int instrumentId1 = 1000, instrumentId2 = 2000;
            var item1 = new MarketDataDto() { Id = id1, Active = true, DataValue=dataValue1, InstrumentId = instrumentId1};
            var item2 = new MarketDataDto() { Id = id2, Active = true, DataValue = dataValue2, InstrumentId = instrumentId2 };
            _mockService.Setup(x => x.GetMarketDataDtoAsync()).ReturnsAsync(new[] {item1, item2} );
            var result = await _sut.Get();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var enumResult = okResult.Value as IEnumerable<MarketDataDto>;
            Assert.NotNull(enumResult);
            var resultValue = enumResult.ToList();
            Assert.Equal(2, resultValue.Count);
            Assert.Equal(id1, resultValue[0].Id);
            Assert.Equal(dataValue1, resultValue[0].DataValue);
            Assert.Equal(instrumentId1, resultValue[0].InstrumentId);
            Assert.Equal(2, resultValue[1].Id);
            Assert.Equal(dataValue2, resultValue[1].DataValue);
            Assert.Equal(instrumentId2, resultValue[1].InstrumentId);

        }

        [Fact]
        public async Task GetInstruments_Should_Return_Internal_Server_Error_on_Exception()
        {
            _mockService.Setup(x => x.GetMarketDataDtoAsync()).Throws(new Exception());
            var result = await _sut.Get();
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);

        }
    }
}
