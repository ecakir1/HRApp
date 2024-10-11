using System.Linq.Expressions;

namespace DAL.Core.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        IEnumerable<T> Find(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        Task<T> SingleOrDefaultDefaultAsync(Expression<Func<T, bool>> filter);

    }
}
