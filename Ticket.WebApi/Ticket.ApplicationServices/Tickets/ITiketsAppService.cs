using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core;
using Ticket.Dto;

namespace Ticket.ApplicationServices.Tickets
{
    public interface ITiketsAppService
    {
        Task<TicketC> AddTicketAsyc(TicketDto ticket);
        Task DeleteTicketAsync(int ticketId);
        Task<TicketC> EditTicketAsync(TicketDto ticket);

        Task<List<TicketDto>> GetTicketsAsync();
        Task<TicketDto> GetTicketsAsync(int ticketId);
    }
}
