using Microsoft.EntityFrameworkCore;
using Shop.ApplicationCore.Interfaces;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Data
{
    public class EFRepository<T> : IRepository<T> where T : class //here we work with EF
    {
        private readonly CatalogContext _dbContext;

        public EFRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await _dbContext.AddAsync<T>(entity);
            _ = await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public List<T> GetALL()
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync(); 
        }

        public T? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync<TId>(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
