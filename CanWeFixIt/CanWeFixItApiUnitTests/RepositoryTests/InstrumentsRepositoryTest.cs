using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CanWeFixItApi.Services;
using CanWeFixItApi.Repositories;
using CanWeFixItService;

namespace CanWeFixItApiUnitTests.RepositoryTests
{
    public class InstrumentsRepositoryTest
    {
        Mock<ILogger<InstrumentsRepository>> _mockLogger;
        Mock<IDatabaseService> _mockDatabaseService;
        InstrumentsRepository _sut;

        public InstrumentsRepositoryTest()
        {
            _mockLogger = new Mock<ILogger<InstrumentsRepository>>();
            _mockDatabaseService = new Mock<IDatabaseService>();
            _sut = new InstrumentsRepository(_mockDatabaseService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetInstruments_Should_Call_DatabaseService_Once()
        {
            _mockDatabaseService.Setup(x => x.Instruments());
            var result = await _sut.GetInstrumentsAsync();
            _mockDatabaseService.Verify(x=>x.Instruments(), Times.Once);
        }

      
        [Fact]
        public async Task GetInstruments_Should_Return_Correct_Result()
        {
            const string name1 = "name1", name2 = "name2", sedol1 = "Sedol1", sedol2 = "Sedol2";
            const int id1 = 1, id2 = 2;
            var item1 = new Instrument() { Id = id1, Active = true, Name = name1, Sedol = sedol1 };
            var item2 = new Instrument() { Id = id2, Active = true, Name = name2, Sedol = sedol2 };
            _mockDatabaseService.Setup(x => x.Instruments()).ReturnsAsync(new[] {item1, item2} );
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
