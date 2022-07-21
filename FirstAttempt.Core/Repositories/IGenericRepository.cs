using System.Linq.Expressions;

namespace FirstAttempt.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int Id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsnc(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);




    }
}
