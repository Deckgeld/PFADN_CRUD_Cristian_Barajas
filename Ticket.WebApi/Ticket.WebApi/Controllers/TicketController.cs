using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.ApplicationServices.Tickets;
using Ticket.Core;
using Ticket.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITiketsAppService _ticketAppService;
        private readonly ILogger<TicketController> _logger;
        Checker checker = new Checker();
        public TicketController(ITiketsAppService ticketAppService, ILogger<TicketController> logger)
        {
            _ticketAppService = ticketAppService;
            _logger = logger;
        }


        // GET: api/<TicketController>
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<TicketDto>> Get()
        {
            List<TicketDto> tickets = await _ticketAppService.GetTicketsAsync();
            _logger.LogInformation("Get all tickets: " + tickets);
            return tickets;
        }

        // GET api/<TicketController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<TicketDto> Get(int id)
        {
            TicketDto ticket = await _ticketAppService.GetTicketsAsync(id);
            _logger.LogInformation("Get ticket: " + id);
            return ticket;
        }

        // POST api/<TicketController>
        [Authorize]
        [HttpPost]
        public async Task<string> Post(TicketDto entity)
        {

            bool isJourneysWebApiSuccessful = await checker.JourneysWebApi(entity.JourneyId);
            bool isPassagersWebApiSuccessful = await checker.PassagersWebApi(entity.PassengerId);
            
            if(isJourneysWebApiSuccessful && isPassagersWebApiSuccessful)
            {
                var Result = await _ticketAppService.AddTicketAsyc(entity);
                _logger.LogInformation("New ticket created: " + entity);
                return Convert.ToString(Result.Id);
            }
            return "PassengerId or JourneyId Not found";

        }

        // PUT api/<TicketController>/5
        [Authorize]
        [HttpPut]
        public async Task Put(TicketDto entity)
        {
            await _ticketAppService.EditTicketAsync(entity);
            _logger.LogInformation("Ticket edited: " + entity);
        }

        // DELETE api/<TicketController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _ticketAppService.DeleteTicketAsync(id);
            _logger.LogInformation("Deleted ticket: " + id);
        }
    }
}
