using Journey.Core.Journey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.DataAccess.Repositories
{
    public class JourneyRepository : Repository<int, JourneyC>
    {
        private readonly JourneysDataContext _context;
        public JourneyRepository(JourneysDataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<JourneyC> UpdateAsync(JourneyC entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }
            try
            {
                var existingEntity = _context.Set<JourneyC>().Local.FirstOrDefault(e => e.Id == entity.Id);
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
