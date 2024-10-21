using DTOsB.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.ServiceB
{
    public interface IGenericServiceB<T>
    {
        public Task<ResultViewB<T>> CreateAsync(T entity);

        public Task<ResultViewB<T>> UpdateAsync(T entity);

        public Task<ResultViewB<T>> DeleteAsync(T entity);

        public Task<T> GetByIDAsync(int id);

        public Task<List<T>> GetAllAsync();

        public Task<int> SaveChangesAsync();
    }
}
