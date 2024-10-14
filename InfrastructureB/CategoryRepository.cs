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
        public async Task<IEnumerable<CategoryB>> GetAllCatAsync()
        {
            return await _dbContext.Categories
                                 .Include(c => c.Translations)
                                 .Include(c => c.ProductCategories)
                                 .ToListAsync();
        }
        public async Task<CategoryB> GetCatByIdAsync(int id)
        {
            return await _dbContext.Categories
                                 .Include(c => c.Translations)
                                 .Include(c => c.ProductCategories)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<CategoryB> GetCatByNameAsync(string categoryName)
        {
            return await _dbContext.Categories
                                 .Include(c => c.Translations)
                                 .Include(c => c.ProductCategories)
                                 .FirstOrDefaultAsync(c => c.Translations
                                 .Any(t => t.CategoryName == categoryName));
        }
        public async Task AddCatAsync(CategoryB category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task UpdateCatAsync(CategoryB category)
        {
            _dbContext.Categories.Update(category);
        }

        public async Task DeleteCatAsync(int id)
        {
            var category = await GetCatByIdAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


    }
}
