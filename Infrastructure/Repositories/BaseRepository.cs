using Domain.IRepositories;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected IDbContext _IDbContext = null;

        protected DbContext _dbContext;

        protected DbSet<TEntity> __dbset;

        protected DbSet<TEntity> DBSet
        {
            get
            {
                if (__dbset is null) __dbset = _dbContext.Set<TEntity>();
                return __dbset;
            }
        }

        public BaseRepository(IDbContext dbContext)
        {
            _IDbContext = dbContext;
            _dbContext = (DbContext)dbContext;
        }




        #region implement IBaseRepository
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = expression is not null ? DBSet.Where(expression) : DBSet;
            return await query.CountAsync();
        }

        public async Task<int> CountAsync(string include, Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query;
            if (!string.IsNullOrEmpty(include))
            {
                query = BuildQueryable(new List<string> { include }, expression);
                return await query.CountAsync();
            }
            return await CountAsync(expression);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            DBSet.Add(entity);
            await _IDbContext.CommitChangeAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            DBSet.AddRange(entities);
            await _IDbContext.CommitChangeAsync();
            return entities;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await DBSet.FindAsync(id);
            if(entity != null) DBSet.Remove(entity);
            await _IDbContext.CommitChangeAsync();
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = expression != null ? DBSet.Where(expression) : DBSet;
            if (query != null) DBSet.RemoveRange(query);
            await _IDbContext.CommitChangeAsync();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = expression != null ? DBSet.Where(expression) : DBSet;
            return query;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DBSet.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DBSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await DBSet.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _IDbContext.CommitChangeAsync();
            return entity;

        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _IDbContext.CommitChangeAsync();
            return entities;
        }
        #endregion


        #region private method
        protected IQueryable<TEntity> BuildQueryable(List<string> includes, Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = DBSet.AsQueryable();
            if(expression != null) query = query.Where(expression);
            if(includes != null && includes.Count > 0)
            {
                foreach(var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
        #endregion
    }
}
