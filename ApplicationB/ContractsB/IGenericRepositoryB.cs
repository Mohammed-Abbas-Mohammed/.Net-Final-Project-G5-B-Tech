using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.ContractsMAFP
{
    public interface IGenericRepositoryB<T>
    {
        public Task<T> CreateAsync(T entity);

        public Task<T> UpdateAsync(T entity);

        public void DeleteAsync(T entity);  //maybe need to return Task<T>

        public Task<T> GetByIDAsync(int id); //maybe need valuetask

        public Task<IQueryable<T>> GetAllAsync();

        public Task<int> SaveChangesAsync();
    }
}
