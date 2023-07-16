using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CanWeFixItApi.Services;
using CanWeFixItApi.Repositories;
using CanWeFixItService;

namespace CanWeFixItApiUnitTests.ServiceTests
{
    public class InstrumentsServiceTest
    {
        Mock<ILogger<InstrumentsService>> _mockLogger;
        Mock<IInstrumentsRepository> _mockRepository;
        InstrumentsService _sut;

        public InstrumentsServiceTest()
        {
            _mockLogger = new Mock<ILogger<InstrumentsService>>();
            _mockRepository = new Mock<IInstrumentsRepository>();
            _sut = new InstrumentsService(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetInstruments_Should_Call_Repository_Once()
        {
            _mockRepository.Setup(x => x.GetInstrumentsAsync());
            var result = await _sut.GetInstrumentsAsync();
            _mockRepository.Verify(x=>x.GetInstrumentsAsync(), Times.Once);
        }

      
        [Fact]
        public async Task GetInstruments_Should_Return_Correct_Result()
        {
            const string name1 = "name1", name2 = "name2", sedol1 = "Sedol1", sedol2 = "Sedol2";
            const int id1 = 1, id2 = 2;
            var item1 = new Instrument() { Id = id1, Active = true, Name = name1, Sedol = sedol1 };
            var item2 = new Instrument() { Id = id2, Active = true, Name = name2, Sedol = sedol2 };
            _mockRepository.Setup(x => x.GetInstrumentsAsync()).ReturnsAsync(new[] {item1, item2} );
            var result = await _sut.GetInstrumentsAsync();
            var list = Assert.IsType<Instrument[]>(result);
            Assert.Equal(2, list.Count());
            Assert.Equal(id1, list[0].Id);
            Assert.Equal(name1, list[0].Name);
            Assert.Equal(sedol1, list[0].Sedol);
            Assert.Equal(id2, list[1].Id);
            Assert.Equal(name2, list[1].Name);
            Assert.Equal(sedol2, list[1].Sedol);

        }

    }
}
