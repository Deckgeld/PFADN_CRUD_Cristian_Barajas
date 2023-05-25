using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Passengers.ApplicationServices.Passengers;
using Microsoft.Extensions.DependencyInjection;
using Passengers.DataAccess.Repositories;


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
            
            var insertFirst = await _passengersAppService.AddPassengerAsyc(new Dto.PassengerDto() { Id= 1, FirstName="Luis", LastName="Barajas", Age=8 });
            var insertSecond = await _passengersAppService.AddPassengerAsyc(new Dto.PassengerDto() { Id= 2, FirstName="Pepe", LastName="Ruiz", Age=8 });
            var insertThird = await _passengersAppService.AddPassengerAsyc(new Dto.PassengerDto() { Id= 3, FirstName="Vane", LastName="Perez", Age=8 });

            var list = await _passengersAppService.GetPassengersAsync();

            Assert.IsNotNull(list);
            Assert.That(3, Is.EqualTo(list.Count));

            //var list = _passengersAppService.GetPassengersAsync();

            Assert.Pass();
        }

        [Order(1)]
        [Test]
        public async Task GetPassagerById_Test()
        {
            var _passengersAppService = server.Host.Services.GetService<IPassengersAppService>();
            var insertNew = await _passengersAppService.AddPassengerAsyc(new Dto.PassengerDto() { Id= 1, FirstName="Luis", LastName="Barajas", Age=8 });
        }


    }
}