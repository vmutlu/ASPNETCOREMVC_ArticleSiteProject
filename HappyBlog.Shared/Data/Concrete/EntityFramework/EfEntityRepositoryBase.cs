using HappyBlog.Shared.Data.Abstract;
using HappyBlog.Shared.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HappyBlog.Shared.Data.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        protected readonly DbContext _dbContext;
        public EfEntityRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);

            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate).ConfigureAwait(false);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? await _dbContext.Set<T>().CountAsync() : await _dbContext.Set<T>().CountAsync(predicate).ConfigureAwait(false);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => { _dbContext.Set<T>().Remove(entity); });
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (includeProperties.Any())
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (includeProperties.Any())
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => { _dbContext.Set<T>().Update(entity); });

            return entity;
        }
    }
}
