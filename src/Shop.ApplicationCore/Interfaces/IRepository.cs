namespace Shop.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T? GetById(int id);

        void Update(T entity);

        public Task<T> AddAsync(T entity);

        List<T> GetALL();

        Task<List<T>> GetAllAsync();
    }
}
