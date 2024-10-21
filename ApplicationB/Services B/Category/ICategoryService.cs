using DTOsB.Category;
using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Category
{
    public interface ICategoryService
    {
        public Task<IEnumerable<GetAllCategoriesDTO>> GetAllCategoriesAsync();
        public Task<CategoryB> GetCategoryByIdAsync(int id);
        public Task<GetAllCategoriesDTO> GetCategoryByNameAsync(string categoryName);
        public Task AddCategoryAsync(CreateOrUpdateCategoriesDTO createCategoryDto);
        public Task UpdateCategoryAsync(int id, CreateOrUpdateCategoriesDTO categoryDto);
        public Task DeleteCategoryAsync(int id);
    }
}
