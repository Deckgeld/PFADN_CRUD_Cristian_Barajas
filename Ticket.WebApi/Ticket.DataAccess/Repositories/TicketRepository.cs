using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core;
using Ticket.DataAccess;
using Ticket.DataAccess.Repositories;

namespace Passengers.DataAccess.Repositories
{
    public class TicketRepository : Repository<int, TicketC>
    {
        private readonly TicketDataContext _context;
        public TicketRepository(TicketDataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<TicketC> UpdateAsync(TicketC entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }
            try
            {
                var existingEntity = _context.Set<TicketC>().Local.FirstOrDefault(e => e.Id == entity.Id);
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
