using Services.Data.Models;
using System.Linq.Expressions;

namespace Services.Data.Repository
{
    public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
		Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
  
		Task<IQueryable<T>> GetAllLazyLoad(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);

	}
}