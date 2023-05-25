using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core;
using Ticket.DataAccess.Repositories;
using Ticket.Dto;

namespace Ticket.ApplicationServices.Tickets
{
    public class TiketsAppService : ITiketsAppService
    {
        private readonly IRepository<int, TicketC> _repository;
        private readonly IMapper _mapper;
        public TiketsAppService(IRepository<int, TicketC> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> AddTicketAsyc(TicketDto ticket)
        {
            var p = _mapper.Map<TicketC>(ticket);
            await _repository.AddAsync(p);
            return p.Id;
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            await _repository.DeleteAsync(ticketId);
        }

        public async Task EditTicketAsync(TicketDto ticket, int id)
        {
            var p = _mapper.Map<TicketC>(ticket);
            p.Id = id;
            await _repository.UpdateAsync(p);
        }

        public async Task<List<TicketDto>> GetTicketsAsync()
        {
            var Tickets = _mapper.Map<List<TicketDto>>(await _repository.GetAll().ToListAsync());
            return Tickets;
        }

        public async Task<TicketDto> GetTicketsAsync(int ticketId)
        {
            return _mapper.Map<TicketDto>(await _repository.GetAsync(ticketId));
        }
    }
}
