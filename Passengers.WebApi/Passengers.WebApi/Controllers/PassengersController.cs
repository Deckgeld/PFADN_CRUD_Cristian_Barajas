using Microsoft.AspNetCore.Mvc;
using Passengers.ApplicationServices.Passengers;
using Passengers.ApplicationServices.Shared.Dto;
using Passengers.Core.Passengers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Passengers.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly IPassengersAppService _passengersAppService;
        private readonly ILogger<PassengersController> _logger;
        public PassengersController(IPassengersAppService passengersAppService, ILogger<PassengersController> logger)
        {
            _passengersAppService = passengersAppService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<PassengersController>
        [HttpGet]
        public async Task<IEnumerable<PassengerDto>> Get()
        {
            List<PassengerDto> passengers = await _passengersAppService.GetPassengersAsync();
            //_logger.LogInformation("Get all passengers: " + passengers);
            return passengers;
        }

        // GET api/<PassengersController>/5
        [HttpGet("{id}")]
        public async Task<PassengerDto> Get(int id)
        {
            PassengerDto passenger = await _passengersAppService.GetPassengersAsync(id);
            //_logger.LogInformation("Get passenger: " + id);
            return passenger;
        }

        // POST api/<PassengersController>
        [HttpPost]
        public async Task<Int32> Post(PassengerDto entity)
        {
            var Result = await _passengersAppService.AddPassengerAsyc(entity);
            //_logger.LogInformation("New passenger created: " + entity);
            return Result;
        }

        // PUT api/<PassengersController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, PassengerDto entity)
        {
            await _passengersAppService.EditPassengerAsync(entity);
            //_logger.LogInformation("Passenger edited: " + entity);
        }

        // DELETE api/<PassengersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _passengersAppService.DeletePassengerAsync(id);
            //_logger.LogInformation("Deleted passenger: " + id);
        }
    }
}
