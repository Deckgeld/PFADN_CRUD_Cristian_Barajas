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
        Task<int> AddJourneyAsyc(JourneyDto journey);
        Task DeleteJourneyAsync(int journeyId);
        Task EditJourneyAsync(JourneyDto journey, int Id);

        Task<List<JourneyDto>> GetJourneysAsync();
        Task<JourneyDto> GetJourneysAsync(int journeyId);
    }
}
