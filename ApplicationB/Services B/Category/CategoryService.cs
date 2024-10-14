using ApplicationB.Contracts_B;
using AutoMapper;
using DTOsB.Category;
using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationB.Services_B.Category.NewFolder
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) {
            _categoryRepository=categoryRepository;
            _mapper=mapper;
        }

        public async Task<IEnumerable<GetAllCategoriesDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAllCategoriesDTO>>(categories);
        }
        public async Task<GetAllCategoriesDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
               
                throw new Exception($"Category with ID {id} not found.");
            }
            return _mapper.Map<GetAllCategoriesDTO>(category);
        }
        public async Task<GetAllCategoriesDTO> GetCategoryByNameAsync(string categoryName)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName);
            if (category == null)
            {
                throw new Exception($"Category with Name {categoryName} not found.");

            }
            return _mapper.Map<GetAllCategoriesDTO>(category);
        }
        public async Task AddCategoryAsync(CreateOrUpdateCategoriesDTO createCategoryDto)
        {
            var category = _mapper.Map<CategoryB>(createCategoryDto);
           
                 await _categoryRepository.AddAsync(category);
                 await _categoryRepository.SaveChangesAsync();
        }
        public async Task UpdateCategoryAsync(int id, CreateOrUpdateCategoriesDTO categoryDto)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }
            _mapper.Map(categoryDto, categoryEntity);
            await _categoryRepository.UpdateAsync(categoryEntity);
            await _categoryRepository.SaveChangesAsync();
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }
            await _categoryRepository.DeleteAsync(id);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}
