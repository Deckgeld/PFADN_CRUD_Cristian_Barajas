using Journey.ApplicationServices.Journey;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MySqlX.XDevAPI.Common;

namespace Journey.UnitTest
{
    [TestFixture]
    public class Tests
    {
        protected TestServer server;

        [OneTimeSetUp]
        public void Setup()
        {
            this.server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        }

        [Order(0)]
        [Test]
        public async Task GetAllPassagers_Test()
        {
            var _JourneysAppService = server.Host.Services.GetService<IJourneyAppService>();

            var insertFirst = await _JourneysAppService.AddJourneyAsyc(new Dto.JourneyDto() { Id = 1, DestinationId = 2, OriginId = 1, Departure = new DateTime(2023, 5, 5, 12, 22, 0), Arrival = DateTime.Now });
            var insertSecond = await _JourneysAppService.AddJourneyAsyc(new Dto.JourneyDto() { Id = 2, DestinationId = 1, OriginId = 2, Departure = new DateTime(2023, 5, 4, 8, 30, 0), Arrival = DateTime.Now });
            var insertThird = await _JourneysAppService.AddJourneyAsyc(new Dto.JourneyDto() { Id = 3, DestinationId = 3, OriginId = 2, Departure = new DateTime(2023, 5, 1,5, 11, 20, 0), Arrival = DateTime.Now });

            var list = await _JourneysAppService.GetJourneysAsync();

            Assert.IsNotNull(list);
            Assert.That(3, Is.EqualTo(list.Count));
        }

        [Order(1)]
        [Test]
        public async Task GetPassagerById_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IJourneyAppService>();

            var insertNew = await _passengersAppService.AddJourneyAsyc(
                new Dto.JourneyDto() { Id = 4, DestinationId = 2, OriginId = 1, Departure = new DateTime(2023, 5, 5, 12, 22, 0), Arrival = DateTime.Now });
            var result = await _passengersAppService.GetJourneysAsync(insertNew.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(insertNew.DestinationId, result.DestinationId);
            Assert.AreEqual(insertNew.OriginId, result.OriginId);
            Assert.AreEqual(insertNew.Departure, result.Departure);
            Assert.AreEqual(insertNew.Arrival, result.Arrival);
        }

        [Order(2)]
        [Test]
        public async Task InsertTicket_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IJourneyAppService>();

            var addTicket1 = await _passengersAppService.AddJourneyAsyc(
                new Dto.JourneyDto() { Id = 5, DestinationId = 5, OriginId = 1, Departure = new DateTime(2023, 5, 5, 12, 22, 0), Arrival = DateTime.Now });
            var getTicket1 = await _passengersAppService.GetJourneysAsync(addTicket1.Id);

            Assert.IsNotNull(addTicket1);
            Assert.AreEqual(addTicket1.DestinationId, getTicket1.DestinationId);
            Assert.AreEqual(addTicket1.OriginId, getTicket1.OriginId);
            Assert.AreEqual(addTicket1.Departure, getTicket1.Departure);
            Assert.AreEqual(addTicket1.Arrival, getTicket1.Arrival);

            var addTicket2 = await _passengersAppService.AddJourneyAsyc(
                new Dto.JourneyDto() { Id = 6, DestinationId = 2, OriginId = 1, Departure = new DateTime(2023, 5, 1, 5, 11, 20, 0), Arrival = DateTime.Now });
            var getTicket2 = await _passengersAppService.GetJourneysAsync(addTicket2.Id);

            Assert.IsNotNull(addTicket2);
            Assert.AreEqual(addTicket2.DestinationId, getTicket2.DestinationId);
            Assert.AreEqual(addTicket2.OriginId, getTicket2.OriginId);
            Assert.AreEqual(addTicket2.Departure, getTicket2.Departure);
            Assert.AreEqual(addTicket2.Arrival, getTicket2.Arrival);
        }

        [Order(4)]
        [Test]
        public async Task EditTicket_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IJourneyAppService>();

            var originalTicket =
                new Dto.JourneyDto() { Id = 7, DestinationId = 2, OriginId = 1, Departure = new DateTime(2023, 5, 1, 5, 11, 20, 0), Arrival = DateTime.Now };
            var insertEntity = await _passengersAppService.AddJourneyAsyc(originalTicket);

            var editedTicket =
                new Dto.JourneyDto() { Id = originalTicket.Id, DestinationId = 3, OriginId = 2, Departure = new DateTime(2022, 2, 20, 5, 12, 20, 0), Arrival = DateTime.Now };
            var updateEntity = await _passengersAppService.EditJourneyAsync(editedTicket);

            var checkUpdate = await _passengersAppService.GetJourneysAsync(originalTicket.Id);

            Assert.IsNotNull(originalTicket);
            Assert.AreNotEqual(originalTicket.DestinationId, checkUpdate.DestinationId);
            Assert.AreNotEqual(originalTicket.OriginId, checkUpdate.OriginId);
            Assert.AreNotEqual(originalTicket.Departure, checkUpdate.Departure);
            Assert.AreNotEqual(originalTicket.Arrival, checkUpdate.Arrival);
        }

        [Order(3)]
        [Test]
        public async Task DeleteTicket_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IJourneyAppService>();

            var addTicket = await _passengersAppService.AddJourneyAsyc(
               new Dto.JourneyDto() { Id = 8, DestinationId = 3, OriginId = 2, Departure = new DateTime(2023, 5, 1, 5, 11, 20, 0), Arrival = DateTime.Now });

            await _passengersAppService.DeleteJourneyAsync(addTicket.Id);
            var checkDelete = await _passengersAppService.GetJourneysAsync(addTicket.Id);

            Assert.IsNull(checkDelete);
        }
    }
}