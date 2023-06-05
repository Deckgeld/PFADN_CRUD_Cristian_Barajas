using Journey.ApplicationServices.Journey;
using Journey.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Journey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyAppService _journeysAppService;
        private readonly ILogger<JourneyController> _logger;
        public JourneyController(IJourneyAppService journeysAppService, ILogger<JourneyController> logger)
        {
            _journeysAppService = journeysAppService;
            _logger = logger;
        }



        // GET: api/<JourneyController>
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<JourneyDto>> Get()
        {
            List<JourneyDto> journeys = await _journeysAppService.GetJourneysAsync();
            _logger.LogInformation("Get all journeys: " + journeys);
            return journeys;
        }

        // GET api/<JourneyController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<JourneyDto> Get(int id)
        {
            JourneyDto journey = await _journeysAppService.GetJourneysAsync(id);
            _logger.LogInformation("Get journey: " + id);
            return journey;
        }

        // POST api/<JourneyController>
        [Authorize]
        [HttpPost]
        public async Task<Int32> Post(JourneyDto entity)
        {
            var Result = await _journeysAppService.AddJourneyAsyc(entity);
            _logger.LogInformation("New journey created: " + entity);
            return Result.Id;
        }

        // PUT api/<JourneyController>/5
        [Authorize]
        [HttpPut]
        public async Task Put(JourneyDto entity)
        {
            await _journeysAppService.EditJourneyAsync(entity);
            _logger.LogInformation("Journey edited: " + entity);
        }

        // DELETE api/<JourneyController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _journeysAppService.DeleteJourneyAsync(id);
            _logger.LogInformation("Deleted journey: " + id);
        }
    }
}
