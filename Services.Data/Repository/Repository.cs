using Microsoft.EntityFrameworkCore;
using Services.Data.Models;
using System.Linq.Expressions;

namespace Services.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ModelDBContext _context;
        private readonly DbSet<T> _db;
        protected readonly DbSet<T> _dbSet;
        public Repository(ModelDBContext context)
        {
            _context = context;

            _dbSet = _context.Set<T>();
        }
		public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;

			if (includes != null)
			{
				foreach (var includeExpression in includes)
				{
					query = query.Include(includeExpression);
				}
			}

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return query.ToList();
		}
	
	public async Task<IEnumerable<T>> GetAllAsync() => await _db.ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _db.FindAsync(id);
        public async Task AddAsync(T entity) { await _db.AddAsync(entity); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(T entity) { _db.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.FindAsync(id);
            if (entity != null)
            {
                _db.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
		public async Task<IQueryable<T>> GetAllLazyLoad(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
		{

			IQueryable<T> query = _dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			// Apply Include expressions if provided
			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await Task.FromResult(query);
		}
	}
}


