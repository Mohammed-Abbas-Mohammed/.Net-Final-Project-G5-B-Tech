using ApplicationB.Contracts_B;
using DbContextB;
using Microsoft.EntityFrameworkCore;
using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureB
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BTechDbContext _dbContext; 
        public CategoryRepository(BTechDbContext dbContextB) {
            _dbContext = dbContextB;
        }

        public async Task AddAsync(CategoryB category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
            }
        }

        public async Task<IEnumerable<CategoryB>> GetAllAsync()
        {
            return await _dbContext.Categories
                                 .Include(c => c.Translations)
                                 .Include(c => c.ProductCategories)
                                 .ToListAsync();
        }

       public async Task<CategoryB> GetByIdAsync(int id)
        {
            return await _dbContext.Categories
                                 .Include(c => c.Translations)
                                 .Include(c => c.ProductCategories)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CategoryB> GetByNameAsync(string categoryName)
        {
            return await _dbContext.Categories
                                 .Include(c => c.Translations)
                                 .Include(c => c.ProductCategories)
                                 .FirstOrDefaultAsync(c => c.Translations
                                 .Any(t => t.CategoryName == categoryName));
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryB category)
        {
            _dbContext.Categories.Update(category);
        }


    }
}
