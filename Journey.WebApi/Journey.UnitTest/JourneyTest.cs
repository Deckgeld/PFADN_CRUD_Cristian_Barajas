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
        public async Task GetAllJourney_Test()
        {
            var _JourneysAppService = server.Host.Services.GetService<IJourneyAppService>();

            var journey1 = await _JourneysAppService.AddJourneyAsyc(
                new Dto.JourneyDto() { Id = 1, DestinationId = 2, OriginId = 1, Departure = new DateTime(2023, 5, 5, 12, 22, 0), Arrival = DateTime.Now });
            var journey2 = await _JourneysAppService.AddJourneyAsyc(
                new Dto.JourneyDto() { Id = 2, DestinationId = 1, OriginId = 2, Departure = new DateTime(2023, 5, 4, 8, 30, 0), Arrival = DateTime.Now });


            var outcome = await _JourneysAppService.GetJourneysAsync();

            Assert.That(outcome, Is.Not.Null);
            Assert.AreEqual(2, outcome.Count);
        }

        [Order(1)]
        [Test]
        public async Task GetJourneyById_Test()
        {
            var _JourneysAppService = server.Host.Services.GetService<IJourneyAppService>();

            var journey1 = await _JourneysAppService.AddJourneyAsyc(
                new Dto.JourneyDto() { Id = 3, DestinationId = 2, OriginId = 1, Departure = new DateTime(2023, 5, 5, 12, 22, 0), Arrival = DateTime.Now });

            var result = await _JourneysAppService.GetJourneysAsync(journey1.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.DestinationId, Is.EqualTo(journey1.DestinationId));
        }

        [Order(2)]
        [Test]
        public async Task InsertJourney_Test()
        {
            var _JourneysAppService = server.Host.Services.GetService<IJourneyAppService>();

            var addPassager = await _JourneysAppService.AddJourneyAsyc(
                new Dto.JourneyDto() { Id = 5, DestinationId = 5, OriginId = 1, Departure = new DateTime(2023, 5, 5, 12, 22, 0), Arrival = DateTime.Now });
            var getPassager = await _JourneysAppService.GetJourneysAsync(addPassager.Id);

            Assert.That(getPassager, Is.Not.Null);
            Assert.That(getPassager.DestinationId, Is.EqualTo(addPassager.DestinationId));
        }

        [Order(4)]
        [Test]
        public async Task EditJourney_Test()
        {
            var _JourneysAppService = server.Host.Services.GetService<IJourneyAppService>();

            var originalEntity = await _JourneysAppService.AddJourneyAsyc(
                new Dto.JourneyDto() { Id = 7, DestinationId = 2, OriginId = 1, Departure = new DateTime(2023, 5, 1, 5, 11, 20, 0), Arrival = DateTime.Now });
            var destinationIdOriginal = originalEntity.DestinationId;

            var updateEntity = await _JourneysAppService.EditJourneyAsync(
                new Dto.JourneyDto() { Id = originalEntity.Id, DestinationId = 3, OriginId = 2, Departure = new DateTime(2022, 2, 20, 5, 12, 20, 0), Arrival = DateTime.Now });

            var checker = await _JourneysAppService.GetJourneysAsync(originalEntity.Id);

            Assert.That(checker.DestinationId, Is.Not.EqualTo(destinationIdOriginal));
        }

        [Order(3)]
        [Test]
        public async Task DeleteJourney_Test()
        {
            var _JourneysAppService = server.Host.Services.GetService<IJourneyAppService>();

            var addPassager = await _JourneysAppService.AddJourneyAsyc(
               new Dto.JourneyDto() { Id = 8, DestinationId = 3, OriginId = 2, Departure = new DateTime(2023, 5, 1, 5, 11, 20, 0), Arrival = DateTime.Now });

            await _JourneysAppService.DeleteJourneyAsync(addPassager.Id);

            var checkDelete = await _JourneysAppService.GetJourneysAsync(addPassager.Id);

            Assert.IsNull(checkDelete);
        }
    }
}