using Passengers.ApplicationServices.Shared.Dto;
using Passengers.Core.Passengers;
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
            CreateMap<Passenger, PassengerDto>();

            CreateMap<PassengerDto, Passenger>();
        }
    }
}
