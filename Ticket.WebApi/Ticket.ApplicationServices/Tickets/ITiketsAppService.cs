using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dto;

namespace Ticket.ApplicationServices.Tickets
{
    public interface ITiketsAppService
    {
        Task<int> AddTicketAsyc(TicketDto ticket);
        Task DeleteTicketAsync(int ticketId);
        Task EditTicketAsync(TicketDto ticket, int id);

        Task<List<TicketDto>> GetTicketsAsync();
        Task<TicketDto> GetTicketsAsync(int ticketId);
    }
}
