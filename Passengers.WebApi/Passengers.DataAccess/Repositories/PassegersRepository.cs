using Passengers.Core.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passengers.DataAccess.Repositories
{
    public class PassegersRepository : Repository<int, PassengerC>
    {
        private readonly PassengersDataContext _context;
        public PassegersRepository(PassengersDataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<PassengerC> UpdateAsync(PassengerC entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }
            try
            {
                var existingEntity = _context.Set<PassengerC>().Local.FirstOrDefault(e => e.Id == entity.Id);
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                }
                else
                {
                    _context.Update(entity);
                }

                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(UpdateAsync)} entity could not be updated: {ex.Message}");
            }
        }
    }
}
