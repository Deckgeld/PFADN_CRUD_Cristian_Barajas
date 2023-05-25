using AutoMapper;
using Journey.Core.Journey;
using Journey.DataAccess.Repositories;
using Journey.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.ApplicationServices.Journey
{
    public class JourneyAppService : IJourneyAppService
    {
        private readonly IRepository<int, JourneyC> _repository;
        private readonly IMapper _mapper;
        public JourneyAppService(IRepository<int, JourneyC> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> AddJourneyAsyc(JourneyDto journey)
        {
            var p = _mapper.Map<JourneyC>(journey);
            await _repository.AddAsync(p);
            return p.Id;
        }

        public async Task DeleteJourneyAsync(int journeyId)
        {
            await _repository.DeleteAsync(journeyId);
        }

        public async Task EditJourneyAsync(JourneyDto journey, int Id)
        {
            var p = _mapper.Map<JourneyC>(journey);
            p.Id= Id;
            await _repository.UpdateAsync(p);
        }

        public async Task<List<JourneyDto>> GetJourneysAsync()
        {
            var journeys = _mapper.Map<List<JourneyDto>>(await _repository.GetAll().ToListAsync());
            return journeys;
        }

        public async Task<JourneyDto> GetJourneysAsync(int journeyId)
        {
            return _mapper.Map<JourneyDto>(await _repository.GetAsync(journeyId));
        }
    }
}
