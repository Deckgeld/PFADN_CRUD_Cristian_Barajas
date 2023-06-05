using Journey.Core.Journey;
using Journey.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.ApplicationServices.Journey
{
    public interface IJourneyAppService
    {
        Task<JourneyC> AddJourneyAsyc(JourneyDto journey);
        Task DeleteJourneyAsync(int journeyId);
        Task<JourneyC> EditJourneyAsync(JourneyDto journey);

        Task<List<JourneyDto>> GetJourneysAsync();
        Task<JourneyDto> GetJourneysAsync(int journeyId);
    }
}
