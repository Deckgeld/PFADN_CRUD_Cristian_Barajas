using Passengers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passengers.ApplicationServices.Passengers
{
    public interface IPassengersAppService
    {
        Task<int> AddPassengerAsyc(PassengerDto passenger);
        Task DeletePassengerAsync(int passengerId);
        Task EditPassengerAsync(PassengerDto passenger, int id);

        Task<List<PassengerDto>> GetPassengersAsync();
        Task<PassengerDto> GetPassengersAsync(int passengerId);
    }
}
