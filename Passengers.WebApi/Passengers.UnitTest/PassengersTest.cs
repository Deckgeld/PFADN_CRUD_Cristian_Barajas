using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Passengers.ApplicationServices.Passengers;
using Microsoft.Extensions.DependencyInjection;
using Passengers.DataAccess.Repositories;
using Passengers.Core.Passengers;
using Passengers.Dto;

namespace Passengers.UnitTest
{
    [TestFixture]
    public class PassengersTest
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
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();
            
            var insertFirst = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id= 1, FirstName="Luis", LastName="Barajas", Age=8 });
            var insertSecond = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id= 2, FirstName="Pepe", LastName="Ruiz", Age=11 });
            var insertThird = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id= 3, FirstName="Vane", LastName="Perez", Age=18 });

            var list = await _passengersAppService.GetPassengersAsync();

            Assert.IsNotNull(list);
            Assert.That(3, Is.EqualTo(list.Count));
        }

        [Order(1)]
        [Test]
        public async Task GetPassagerById_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();

            var insertNew = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id= 4, FirstName="Luis", LastName="Barajas", Age=8 });
            var result = await _passengersAppService.GetPassengersAsync(insertNew.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(insertNew.FirstName, result.FirstName);
            Assert.AreEqual(insertNew.LastName, result.LastName);
            Assert.AreEqual(insertNew.Age, result.Age);
        }

        [Order(2)]
        [Test]
        public async Task InsertTicket_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();

            var addTicket1 = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id= 5, FirstName="Luis", LastName="Barajas", Age=8 });
            var getTicket1 = await _passengersAppService.GetPassengersAsync(addTicket1.Id);

            Assert.IsNotNull(addTicket1);
            Assert.AreEqual(addTicket1.FirstName, getTicket1.FirstName);
            Assert.AreEqual(addTicket1.LastName, getTicket1.LastName);
            Assert.AreEqual(addTicket1.Age, getTicket1.Age);

            var addTicket2 = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id= 6, FirstName="Pedro", LastName="Castro", Age=18 });
            var getTicket2 = await _passengersAppService.GetPassengersAsync(addTicket2.Id);

            Assert.IsNotNull(addTicket2);
            Assert.AreEqual(addTicket2.FirstName, getTicket2.FirstName);
            Assert.AreEqual(addTicket2.LastName, getTicket2.LastName);
            Assert.AreEqual(addTicket2.Age, getTicket2.Age);
        }

        [Order(3)]
        [Test]
        public async Task EditTicket_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();

            var originalTicket = 
                new Dto.PassengerDto() { Id = 7, FirstName = "Luis", LastName = "Barajas", Age = 8 };
            var insertEntity = await _passengersAppService.AddPassengerAsyc(originalTicket);

            var editedTicket = 
                new PassengerDto() { Id = insertEntity.Id, FirstName = "Pepe", LastName = "Castillo", Age = 18 };
            var updateEntity = await _passengersAppService.EditPassengerAsync(editedTicket);

            var checkUpdate = await _passengersAppService.GetPassengersAsync(originalTicket.Id);

            Assert.IsNotNull(originalTicket);
            Assert.AreNotEqual(originalTicket.FirstName, checkUpdate.FirstName);
            Assert.AreNotEqual(originalTicket.LastName, checkUpdate.LastName);
            Assert.AreNotEqual(originalTicket.Age, checkUpdate.Age);
        }

        [Order(4)]
        [Test]
        public async Task DeleteTicket_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();

            var addTicket = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id = 8, FirstName = "Luis", LastName = "Barajas", Age = 8 });

            await _passengersAppService.DeletePassengerAsync(addTicket.Id);
            var checkDelete = await _passengersAppService.GetPassengersAsync(addTicket.Id);

            Assert.IsNull(checkDelete);
        }

    }
}