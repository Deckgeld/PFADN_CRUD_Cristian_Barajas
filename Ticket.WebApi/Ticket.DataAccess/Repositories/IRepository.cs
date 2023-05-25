using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.DataAccess.Repositories
{
    public interface IRepository<TId, TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> GetAsync(TId id);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);
    }
}
