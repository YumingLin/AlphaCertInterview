using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CanWeFixItApi.Controllers;
using CanWeFixItApi.Services;
using CanWeFixItService;

namespace CanWeFixItApiUnitTests.ControllerTests
{
    public class ValuationsControllerTest
    {
        Mock<ILogger<ValuationsController>> _mockLogger;
        Mock<IValuationsService> _mockService;
        ValuationsController _sut;

        public ValuationsControllerTest()
        {
            _mockLogger = new Mock<ILogger<ValuationsController>>();
            _mockService = new Mock<IValuationsService>();
            _sut = new ValuationsController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetMarketValuation_Should_Call_Service_Once()
        {
            _mockService.Setup(x => x.GetMarketValuationAsync());
            var result = await _sut.Get();
            _mockService.Verify(x=>x.GetMarketValuationAsync(), Times.Once);
        }

      
        [Fact]
        public async Task GetMarketValuation_Should_Return_Correct_Result()
        {
            const long total = 1000;
            const string name = "test market name";
            var item1 = new MarketValuation() {Name = name, Total = total };
            _mockService.Setup(x => x.GetMarketValuationAsync()).ReturnsAsync(new[] { item1 });
            var result = await _sut.Get();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var enumResult = okResult.Value as IEnumerable<MarketValuation>;
            Assert.NotNull(enumResult);
            var resultValue = enumResult.ToList();
            Assert.Single(resultValue);
            Assert.Equal(name, resultValue[0].Name);
            Assert.Equal(total, resultValue[0].Total);
            

        }

        [Fact]
        public async Task GetMarketValuation_Should_Return_Internal_Server_Error_on_Exception()
        {
            _mockService.Setup(x => x.GetMarketValuationAsync()).Throws(new Exception());
            var result = await _sut.Get();
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);

        }
    }
}
