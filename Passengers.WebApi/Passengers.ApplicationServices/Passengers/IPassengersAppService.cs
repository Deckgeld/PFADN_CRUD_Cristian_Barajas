using Passengers.Core.Passengers;
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
        Task<PassengerC> AddPassengerAsyc(PassengerDto passenger);
        Task DeletePassengerAsync(int passengerId);
        Task<PassengerC> EditPassengerAsync(PassengerDto passenger);

        Task<List<PassengerDto>> GetPassengersAsync();
        Task<PassengerDto> GetPassengersAsync(int passengerId);
    }
}
