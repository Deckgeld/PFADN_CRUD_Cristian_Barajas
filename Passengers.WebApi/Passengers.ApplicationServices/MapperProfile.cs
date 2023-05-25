using Passengers.Core.Passengers;
using Passengers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passengers.ApplicationServices
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile() 
        {
            CreateMap<PassengerC, PassengerDto>();

            CreateMap<PassengerDto, PassengerC>();
        }
    }
}
