using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Contracts_B
{
    public interface IGenericRepositoryB<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();    
        Task<T> GetByIdAsync(int id);           
        Task AddAsync(T entity);                
        Task UpdateAsync(T entity);             
        Task DeleteAsync(int id);               
        Task SaveChangesAsync();
    }
}
