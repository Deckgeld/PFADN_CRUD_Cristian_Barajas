using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MySqlX.XDevAPI.Common;
using Passengers.UnitTest;
using Ticket.ApplicationServices.Tickets;
using Ticket.Core;

namespace Ticket.UnitTest
{
    [TestFixture]
    public class TicketsTest
    {
        protected TestServer server;

        [OneTimeSetUp]
        public void Setup()
        {
            this.server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        }

        [Order(0)]
        [Test]
        public async Task GetAllTickets_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();
            
            var ticket1 = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 1, PassengerId = 1, JourneyId = 1, Seat = 1 });
            var ticket2 = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 2, PassengerId = 2, JourneyId = 2, Seat = 2 });

            var outcome = await _ticketsAppService.GetTicketsAsync();

            Assert.That(outcome, Is.Not.Null);
            Assert.That(outcome.Count, Is.EqualTo(2));

        }

        [Order(1)]
        [Test]
        public async Task GetTicketById_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();

            var ticket = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 3, PassengerId = 3, JourneyId = 3, Seat = 3 });
            var result = await _ticketsAppService.GetTicketsAsync(ticket.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PassengerId, Is.EqualTo(ticket.PassengerId));
        }

        [Order(2)]
        [Test]
        public async Task InsertTicket_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();

            var addTicket = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 4, PassengerId = 1, JourneyId = 1, Seat = 1 });
            var getTicket = await _ticketsAppService.GetTicketsAsync(addTicket.Id);

            Assert.That(getTicket, Is.Not.Null);
            Assert.That(getTicket.JourneyId, Is.EqualTo(addTicket.JourneyId));
        }

        [Order(3)]
        [Test]
        public async Task EditTicket_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();
            
            var originalEntity = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 5, PassengerId = 4, JourneyId = 4, Seat = 4 });
            var PassengerIdOriginal = originalEntity.PassengerId;

            var updateEntity = await _ticketsAppService.EditTicketAsync(
                new Ticket.Dto.TicketDto() { Id = originalEntity.Id, PassengerId = 3, JourneyId = 3, Seat = 3 });

            var checker = await _ticketsAppService.GetTicketsAsync(originalEntity.Id);

            Assert.That(checker.PassengerId, Is.Not.EqualTo(PassengerIdOriginal));
        }

        [Order(4)]
        [Test]
        public async Task DeleteTicket_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();

            var addTicket = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 6, PassengerId = 7, JourneyId = 2, Seat = 5 });

            await _ticketsAppService.DeleteTicketAsync(addTicket.Id);

            var checkDelete = await _ticketsAppService.GetTicketsAsync(addTicket.Id);

            Assert.IsNull(checkDelete);
        }

    }
}