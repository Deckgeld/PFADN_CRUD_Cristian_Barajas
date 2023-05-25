using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core;
using Ticket.Dto;

namespace Ticket.ApplicationServices
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<TicketC, TicketDto>();

            CreateMap<TicketDto, TicketC>();
        }
    }
}