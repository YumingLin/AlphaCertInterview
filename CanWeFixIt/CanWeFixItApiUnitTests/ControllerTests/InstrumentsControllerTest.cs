using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CanWeFixItApi.Controllers;
using CanWeFixItApi.Services;
using CanWeFixItService;

namespace CanWeFixItApiUnitTests.ControllerTests
{
    public class InstrumentsControllerTest
    {
        Mock<ILogger<InstrumentsController>> _mockLogger;
        Mock<IInstrumentsService> _mockService;
        InstrumentsController _sut;

        public InstrumentsControllerTest()
        {
            _mockLogger = new Mock<ILogger<InstrumentsController>>();
            _mockService = new Mock<IInstrumentsService>();
            _sut = new InstrumentsController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetInstruments_Should_Call_Service_Once()
        {
            _mockService.Setup(x => x.GetInstrumentsAsync());
            var result = await _sut.Get();
            _mockService.Verify(x=>x.GetInstrumentsAsync(), Times.Once);
        }

      
        [Fact]
        public async Task GetInstruments_Should_Return_Correct_Result()
        {
            const string name1 = "name1", name2 = "name2", sedol1 = "Sedol1", sedol2 = "Sedol2";
            const int id1 = 1, id2 = 2;
            var item1 = new Instrument() { Id = id1, Active = true, Name = name1, Sedol = sedol1 };
            var item2 = new Instrument() { Id = id2, Active = true, Name = name2, Sedol = sedol2 };
            _mockService.Setup(x => x.GetInstrumentsAsync()).ReturnsAsync(new[] {item1, item2} );
            var result = await _sut.Get();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var enumResult = okResult.Value as IEnumerable<Instrument>;
            Assert.NotNull(enumResult);
            var resultValue = enumResult.ToList();
            Assert.Equal(2, resultValue.Count);
            Assert.Equal(id1, resultValue[0].Id);
            Assert.Equal(name1, resultValue[0].Name);
            Assert.Equal(sedol1, resultValue[0].Sedol);
            Assert.Equal(id2, resultValue[1].Id);
            Assert.Equal(name2, resultValue[1].Name);
            Assert.Equal(sedol2, resultValue[1].Sedol);

        }

        [Fact]
        public async Task GetInstruments_Should_Return_Internal_Server_Error_on_Exception()
        {
            _mockService.Setup(x => x.GetInstrumentsAsync()).Throws(new Exception());
            var result = await _sut.Get();
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);

        }

    }
}
