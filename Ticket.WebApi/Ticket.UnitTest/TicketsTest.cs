using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MySqlX.XDevAPI.Common;
using Passengers.UnitTest;
using Ticket.ApplicationServices.Tickets;

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
        public async Task GetAllPassagers_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();
            
            var insertFirst = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id=1, PassengerId = 1, JourneyId =1, Seat=1 });
            var insertSecond = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id=2, PassengerId = 2, JourneyId =2, Seat=2 });
            var insertThird = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id=3, PassengerId = 3, JourneyId =3, Seat=3 });

            var list = await _ticketsAppService.GetTicketsAsync();

            Assert.IsNotNull(list);
            Assert.That(3, Is.EqualTo(list.Count));
        }

        [Order(1)]
        [Test]
        public async Task GetPassagerById_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();

            var insertNew = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 4, PassengerId = 3, JourneyId = 3, Seat = 3 });
            var result = await _ticketsAppService.GetTicketsAsync(insertNew.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(insertNew.PassengerId, result.PassengerId);
            Assert.AreEqual(insertNew.JourneyId, result.JourneyId);
            Assert.AreEqual(insertNew.Seat, result.Seat);
        }

        [Order(2)]
        [Test]
        public async Task InsertTicket_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();

            var addTicket1 = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 5, PassengerId = 1, JourneyId = 1, Seat = 1 });
            var getTicket1 = await _ticketsAppService.GetTicketsAsync(addTicket1.Id);

            Assert.IsNotNull(addTicket1);
            Assert.AreEqual(addTicket1.PassengerId, getTicket1.PassengerId);
            Assert.AreEqual(addTicket1.JourneyId, getTicket1.JourneyId);
            Assert.AreEqual(addTicket1.Seat, getTicket1.Seat);

            var addTicket2 = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 6, PassengerId = 2, JourneyId = 2, Seat = 2 });
            var getTicket2 = await _ticketsAppService.GetTicketsAsync(addTicket2.Id);

            Assert.IsNotNull(addTicket2);
            Assert.AreEqual(addTicket2.PassengerId, getTicket2.PassengerId);
            Assert.AreEqual(addTicket2.JourneyId, getTicket2.JourneyId);
            Assert.AreEqual(addTicket2.Seat, getTicket2.Seat);
        }

        [Order(3)]
        [Test]
        public async Task EditTicket_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();

            var originalTicket =
                new Ticket.Dto.TicketDto() { Id = 7, PassengerId = 4, JourneyId = 4, Seat = 4 };
            var insertEntity = await _ticketsAppService.AddTicketAsyc(originalTicket);

            var editedTicket =
                new Ticket.Dto.TicketDto() { Id = originalTicket.Id, PassengerId = 3, JourneyId = 3, Seat = 3 };
            var updateEntity = await _ticketsAppService.EditTicketAsync(editedTicket);

            var checkUpdate = await _ticketsAppService.GetTicketsAsync(originalTicket.Id);

            Assert.IsNotNull(originalTicket);
            Assert.AreNotEqual(originalTicket.PassengerId, checkUpdate.PassengerId);
            Assert.AreNotEqual(originalTicket.JourneyId, checkUpdate.JourneyId);
            Assert.AreNotEqual(originalTicket.Seat, checkUpdate.Seat);
        }

        [Order(4)]
        [Test]
        public async Task DeleteTicket_Test()
        {
            var _ticketsAppService = server.Host.Services.GetService<ITiketsAppService>();

            var addTicket = await _ticketsAppService.AddTicketAsyc(
                new Ticket.Dto.TicketDto() { Id = 9, PassengerId = 7, JourneyId = 2, Seat = 5 });

            await _ticketsAppService.DeleteTicketAsync(addTicket.Id);
            var checkDelete = await _ticketsAppService.GetTicketsAsync(addTicket.Id);

            Assert.IsNull(checkDelete);
        }

    }
}