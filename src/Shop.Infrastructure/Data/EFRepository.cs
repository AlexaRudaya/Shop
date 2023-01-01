using Microsoft.EntityFrameworkCore;
using Shop.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Data
{
    public class EFRepository<T> : IRepository<T> where T : class //here we work with EF
    {
        private readonly CatalogContext _dbContext;

        public EFRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext;
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

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
