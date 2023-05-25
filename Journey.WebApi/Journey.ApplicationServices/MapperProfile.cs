using Journey.Core.Journey;
using Journey.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.ApplicationServices
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<JourneyC, JourneyDto>();

            CreateMap<JourneyDto, JourneyC>();
        }
    }
}
