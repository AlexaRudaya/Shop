using System.Linq.Expressions;

namespace Shop.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public T? GetById(int id);

        public void Update(T entity);

        public Task<T> AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public List<T> GetALL();

        public Task<List<T>> GetAllAsync();

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
