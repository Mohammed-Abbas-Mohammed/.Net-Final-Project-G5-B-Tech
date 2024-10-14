using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Contracts_B
{
    public interface ICategoryRepository : IGenericRepositoryB<CategoryB>
    {
        //Task<IEnumerable<CategoryB>> GetAllAsync();
        //Task<CategoryB> GetByIdAsync(int id);
        Task<CategoryB> GetByNameAsync(string categoryName);
        //Task AddAsync(CategoryB category);
        //Task UpdateAsync(CategoryB category);
        //Task DeleteAsync(int id);
        //Task SaveChangesAsync();
    }
}
