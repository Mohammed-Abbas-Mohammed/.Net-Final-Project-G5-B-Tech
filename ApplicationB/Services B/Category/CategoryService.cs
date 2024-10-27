using ApplicationB.Contracts_B.Category;
using ApplicationB.Contracts_B.General;
using ApplicationB.Services_B.General;
using AutoMapper;
using DTOsB.Category;
using DTOsB.Product;
using Microsoft.AspNetCore.Http;
using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILanguageService _languageService;
        private readonly ILanguageRepository  _languageRepository;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IUserService userService, ILanguageService languageService, ILanguageRepository languageRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _userService = userService;
            _languageService = languageService;
            _languageRepository = languageRepository;

        }

        public async Task<IEnumerable<GetAllCategoriesDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var activeCategories = categories.Where(c => !c.IsDeleted);
            return _mapper.Map<IEnumerable<GetAllCategoriesDTO>>(activeCategories);

        }
        public async Task<IEnumerable<GetAllCategoriesDTO>> GetCategoriesByLanguageAsync(int languageId)
        {
           var categories = await _categoryRepository.GetByLanguageAsync(languageId);
            return _mapper.Map<IEnumerable<GetAllCategoriesDTO>>(categories);
        }

        public async Task<CategoryB> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {

                throw new Exception($"Category with ID {id} not found.");
            }
            return (category);
        }
        public async Task<IEnumerable<GetAllCategoriesDTO>> GetCategoryByNameAsync(string categoryName)
        {
            var categories = await _categoryRepository.GetAllAsync(); 
            return categories.Where(c => c.Translations.Any(t => t.CategoryName.ToLower().Contains(categoryName.ToLower())))
                             .Select(c => _mapper.Map<GetAllCategoriesDTO>(c))
                             .ToList();
        }
        public async Task AddCategoryAsync(CreateOrUpdateCategoriesDTO createCategoryDto, IFormFile imageFile)
        {
            // Check if the category name already exists
            var existingCategory = await _categoryRepository
                .AnyAsync(c => c.Translations.Any(t => t.CategoryName.ToLower() == createCategoryDto.Translations.First().CategoryName.ToLower()));

            if (existingCategory)
            {
                throw new Exception("A category with the same name already exists.");
            }

            // Handle image upload (if provided)
            string imageUrl = null;
            if (imageFile != null)
            {
                 imageUrl = await HandleImageUploadAsync(imageFile);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    throw new Exception("Image upload failed. Please provide a valid image.");
                }
            }

            // Set user and language information
            var currentUserId = _userService.GetCurrentUserId();
            var currentLanguage = _languageService.GetCurrentLanguageCode();

            // Map the DTO to the entity
            var category = _mapper.Map<CategoryB>(createCategoryDto);
            category.CreatedBy = currentUserId;
            category.UpdatedBy = currentUserId;
            category.ImageUrl = imageUrl;

            // Initialize product category list
            category.ProductCategories = new List<ProductCategoryB>();

            if (createCategoryDto.Translations == null || !createCategoryDto.Translations.Any())
            {
                throw new Exception("Translations list cannot be null or empty.");
            }

            foreach (var translation in createCategoryDto.Translations)
            {
                if (translation == null)
                {
                    throw new Exception("Translation cannot be null.");
                }

                // Validate and set LanguageId
                if (translation.LanguageId == null || translation.LanguageId <= 0)
                {
                    translation.LanguageId = 2; // Default to a specific language if needed
                   
                }

                var languageExists = await _languageRepository.AnyAsync(l => l.Id == translation.LanguageId);
                if (!languageExists)
                {
                    throw new Exception($"Language with ID {translation.LanguageId} does not exist.");
                }

                if (string.IsNullOrEmpty(translation.CategoryName))
                {
                    throw new Exception("Category name cannot be null or empty.");
                }

                // Add product category relationship
                if (createCategoryDto.ProductIds != null && createCategoryDto.ProductIds.Any())
                {
                    foreach (var productId in createCategoryDto.ProductIds)
                    {
                        // Create an instance of ProductCategoryDto
                        var productCategoryDto = new ProductCategoryDto
                        {
                            ProductId = productId, // Use the variable directly here
                            Category = _mapper.Map<GetAllCategoriesDTO>(category), // Map category if needed
                            IsMainCategory = translation.IsMainCategory // Access IsMainCategory through translation
                        };

                        // Create the ProductCategoryB entity
                        var productCategoryEntity = new ProductCategoryB
                        {
                            ProductId = productCategoryDto.ProductId, // Access ProductId from the instance
                            Category = category, // Use the category instance
                            IsMainCategory = productCategoryDto.IsMainCategory // Access IsMainCategory from the instance
                        };
                        category.ProductCategories.Add(productCategoryEntity);

                    }
                }
            }

            // Save the new category
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

   
        public async Task UpdateCategoryAsync(int id, CreateOrUpdateCategoriesDTO categoryDto, IFormFile imageFile)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            var existingImageUrl = categoryEntity.ImageUrl;

            // Handle image upload (convert IFormFile to a URL or save path)
            if (imageFile != null)
            {
                try
                {
                    var newImageUrl = await HandleImageUploadAsync(imageFile);
                    categoryEntity.ImageUrl = newImageUrl; // Update the image if new image uploaded
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error uploading image: {ex.Message}");
                    // Revert to existing image URL if the upload fails
                    categoryEntity.ImageUrl = existingImageUrl;
                    throw new Exception("Image upload failed.");
                }
            }
            else
            {
                // If no new image is uploaded, retain the old image or update with provided URL
                categoryEntity.ImageUrl = !string.IsNullOrEmpty(categoryDto.ImageUrl) ? categoryDto.ImageUrl : existingImageUrl;
            }


            // Update the user and updated date
            categoryEntity.UpdatedBy = _userService.GetCurrentUserId();
            categoryEntity.Updated = DateTime.Now;

            // Clear existing translations and product categories
            categoryEntity.Translations.Clear();
            categoryEntity.ProductCategories.Clear();

            // Validate and update translations using AutoMapper
            if (categoryDto.Translations == null || !categoryDto.Translations.Any())
            {
                throw new Exception("Translations list cannot be null or empty.");
            }
            // Update translations using AutoMapper
            foreach (var translationDto in categoryDto.Translations)
            {
                if (string.IsNullOrEmpty(translationDto.CategoryName))
                {
                    throw new Exception("Category name cannot be null or empty.");
                }

                // Map translation DTO to entity
                var translation = _mapper.Map<CategoryTranslationB>(translationDto);
                categoryEntity.Translations.Add(translation);

                // Add product category relationships
                if (categoryDto.ProductIds != null && categoryDto.ProductIds.Any())
                {
                    foreach (var productId in categoryDto.ProductIds)
                    {
                        var productCategory = new ProductCategoryB
                        {
                            ProductId = productId,
                            Category = categoryEntity,
                            IsMainCategory = categoryDto.Translations.First().IsMainCategory // Assuming the main category flag is derived from the first translation
                        };
                        categoryEntity.ProductCategories.Add(productCategory);
                    }
                }
            }

            // Update the category in the repository
            await _categoryRepository.UpdateAsync(categoryEntity);

            // Save changes
            await _categoryRepository.SaveChangesAsync();
        }


        public async Task DeleteCategoryAsync(int id)
            {
                var categoryEntity = await _categoryRepository.GetByIdAsync(id);
                if (categoryEntity == null)
                {
                    throw new KeyNotFoundException($"Category with ID {id} not found.");
                }
                 // Check if the category is linked to any products or subcategories
                 if (categoryEntity.ProductCategories.Any())
                 {
                   // You can choose to throw an exception, log a warning, or handle it accordingly
                   throw new InvalidOperationException($"Category with ID {id} cannot be deleted because it has associated products or subcategories.");
                  }
                     // soft delete
                 categoryEntity.IsDeleted = true;

                await _categoryRepository.UpdateAsync(categoryEntity);
                await _categoryRepository.SaveChangesAsync();
            }

         public async Task<string> HandleImageUploadAsync(IFormFile imageFile)
            {
                if (imageFile == null || imageFile.Length == 0)
                    return null;

                // Logic to save the image to a folder and return the file path or URL
                var fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                var fileExtension = Path.GetExtension(imageFile.FileName);
                var newFileName = $"{fileName}_{DateTime.Now.Ticks}{fileExtension}";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ImageUrls/categories", newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            Console.WriteLine($"Image saved to: {filePath}"); 

            return $"/ImageUrls/categories/{newFileName}"; // Return the relative URL or path        
            }
        public async Task<List<ProductCategoryDto>> GetProductsByCategoryNameAsync(string categoryName)
        {
            // Get categories by name using your existing GetByNameAsync method
            var categories = await _categoryRepository.GetByNameAsync(categoryName);

            if (!categories.Any())
            {
                throw new Exception($"No categories found with name {categoryName}.");
            }

            // Extract products from all matching categories
            var productCategories = categories
                                          .SelectMany(c => c.ProductCategories)
                                          .Distinct()
                                          .ToList();

            // Map products to ProductDto
            var productCategoryDtos = _mapper.Map<List<ProductCategoryDto>>(productCategories);

            return productCategoryDtos;
        }

        public async Task<List<ProductCategoryDto>> GetProductsByCategoryIdAsync(int categoryId)
        {
            // Get the category by its ID, including the related products
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                throw new Exception($"Category with ID {categoryId} not found.");
            }

            // Extract product details from the ProductCategories relationship
            var productCategories = category.ProductCategories.ToList();

            var productCategoryDtos = _mapper.Map<List<ProductCategoryDto>>(productCategories);

            //var products = category.ProductCategories
            //                       .Select(pc => pc.Product)
            //                       .ToList();

            //// Map products to ProductDto
            //var productDtos = _mapper.Map<List<ProductDto>>(products);
            //return productDtos;
            return productCategoryDtos;
        }

        public async Task<List<GetAllCategoriesDTO>> GetMainCategoriesAsync()
        {

            var mainCategories = await _categoryRepository.GetAllAsync();

            var filteredMainCategories = mainCategories
                .Where(c => c.ProductCategories.Any(pc => pc.IsMainCategory))
                .ToList();

            var mainCategoriesDto = _mapper.Map<List<GetAllCategoriesDTO>>(filteredMainCategories);

            return mainCategoriesDto;
        }

    }
} 

