using ATOS.DataAccess.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Passengers.ApplicationServices.Shared.Dto;
using Passengers.Core.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passengers.ApplicationServices.Passengers
{
    public class PassengersAppService : IPassengersAppService
    {
        private readonly IRepository<int, Passenger> _repository;
        private readonly IMapper _mapper;
        public PassengersAppService(IRepository<int, Passenger> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddPassengerAsyc(PassengerDto passenger)
        {
            var p = _mapper.Map<Passenger>(passenger);
            await _repository.AddAsync(p);
            return p.Id;
        }

        public async Task DeletePassengerAsync(int passengerId)
        {
            await _repository.DeleteAsync(passengerId);
        }

        public async Task EditPassengerAsync(PassengerDto passenger)
        {
            var p = _mapper.Map<Passenger>(passenger);
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
