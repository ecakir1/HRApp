using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Core.IRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly HRManagementDbContext Context;

        public GenericRepository(HRManagementDbContext context)
        {

            Context = context;
        }

        public virtual async Task AddAsync(T item)
        {
            await Context.Set<T>().AddAsync(item);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);

        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return Context.Set<T>().Where(filter);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async  Task<T> GetByIdAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual void Remove(T Entity)
        {
            Context.Set<T>().Remove(Entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public virtual async Task<T> SingleOrDefaultDefaultAsync(Expression<Func<T, bool>> filter)
        {
            return await Context.Set<T>().SingleOrDefaultAsync(filter);
        }
    }
}
