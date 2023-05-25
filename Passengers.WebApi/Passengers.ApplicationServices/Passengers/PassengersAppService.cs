using Passengers.DataAccess.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Passengers.Dto;
using Passengers.ApplicationServices.Passengers;
using Passengers.Core.Passengers;

namespace Passengers.ApplicationServices.Passengers
{
    public class PassengersAppService : IPassengersAppService
    {
        private readonly IRepository<int, PassengerC> _repository;
        private readonly IMapper _mapper;
        public PassengersAppService(IRepository<int, PassengerC> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddPassengerAsyc(PassengerDto passenger)
        {
            PassengerC p = _mapper.Map<PassengerC>(passenger);
            await _repository.AddAsync(p);
            return p.Id;
        }

        public async Task DeletePassengerAsync(int passengerId)
        {
            await _repository.DeleteAsync(passengerId);
        }

        public async Task EditPassengerAsync(PassengerDto passenger, int id)
        {
            var p = _mapper.Map<PassengerC>(passenger);
            p.Id = id;
            await _repository.UpdateAsync(p);
        }

        public async Task<List<PassengerDto>> GetPassengersAsync()
        {
            var users = _mapper.Map<List<PassengerDto>>(await _repository.GetAll().ToListAsync());
            return users;
        }

        public async Task<PassengerDto> GetPassengersAsync(int passengerId)
        {
            return _mapper.Map<PassengerDto>(await _repository.GetAsync(passengerId));
        }
    }
}
