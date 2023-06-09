using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Passengers.ApplicationServices.Passengers;
using Microsoft.Extensions.DependencyInjection;
using Passengers.DataAccess.Repositories;
using Passengers.Core.Passengers;
using Passengers.Dto;
using MySqlX.XDevAPI.Common;

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

            var passager1 = await _passengersAppService.AddPassengerAsyc(
                new PassengerDto() { Id = 1, FirstName = "Luis", LastName = "Barajas", Age = 8 });
            var passager2 = await _passengersAppService.AddPassengerAsyc(
                new PassengerDto() { Id = 2, FirstName = "Alejandro", LastName = "Barajas", Age = 38 });

            var outcome = await _passengersAppService.GetPassengersAsync();

            Assert.That(outcome, Is.Not.Null);
            Assert.That(outcome.Count, Is.EqualTo(2));
        }

        [Order(1)]
        [Test]
        public async Task GetPassagerById_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();

            var passager1 = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id= 3, FirstName="Luis", LastName="Barajas", Age=8 });
            var result = await _passengersAppService.GetPassengersAsync(passager1.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.FirstName, Is.EqualTo(passager1.FirstName));
        }

        [Order(2)]
        [Test]
        public async Task InsertPassager_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();

            var addPassager = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id = 4, FirstName = "Luis", LastName = "Barajas", Age = 8 });
            var getPassager = await _passengersAppService.GetPassengersAsync(addPassager.Id);

            Assert.That(getPassager, Is.Not.Null);
            Assert.That(getPassager.FirstName, Is.EqualTo(addPassager.FirstName));
        }

        [Order(3)]
        [Test]
        public async Task EditPassager_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();

            var originalEntity = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id = 5, FirstName = "Luis", LastName = "Barajas", Age = 8 });
            var nameOriginal = originalEntity.FirstName;

            var updateEntity = await _passengersAppService.EditPassengerAsync(
                new PassengerDto() { Id = originalEntity.Id, FirstName = "Pepe", LastName = "Castillo", Age = 18 });

            var checker = await _passengersAppService.GetPassengersAsync(originalEntity.Id);

            Assert.That(checker.FirstName, Is.Not.EqualTo(nameOriginal));
            
        }

        [Order(4)]
        [Test]
        public async Task DeletePassager_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();

            var addPassager = await _passengersAppService.AddPassengerAsyc(
                new Dto.PassengerDto() { Id = 6, FirstName = "Luis", LastName = "Barajas", Age = 8 });

            await _passengersAppService.DeletePassengerAsync(addPassager.Id);

            var checkDelete = await _passengersAppService.GetPassengersAsync(addPassager.Id);

            Assert.IsNull(checkDelete);

        }

    }
}