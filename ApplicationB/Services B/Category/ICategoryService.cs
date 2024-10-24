using DTOsB.Category;
using Microsoft.AspNetCore.Http;
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
        public Task<IEnumerable<GetAllCategoriesDTO>> GetCategoryByNameAsync(string categoryName);
        public Task AddCategoryAsync(CreateOrUpdateCategoriesDTO createCategoryDto, IFormFile imageFile);
        public Task UpdateCategoryAsync(int id, CreateOrUpdateCategoriesDTO categoryDto, IFormFile imageFile);
        public Task DeleteCategoryAsync(int id);
        public Task<string> HandleImageUploadAsync(IFormFile imageFile);
    }
}
