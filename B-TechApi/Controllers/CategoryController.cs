using ApplicationB.Services_B.Category;
using ApplicationB.Services_B.General;
using ApplicationB.Services_B.Product;
using AutoMapper;
using DTOsB.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B_Tech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public CategoryController(IProductService productService, ICategoryService categoryService, ILanguageService languageService, IMapper mapper) {
            _productService = productService;
            _categoryService = categoryService;
            _languageService = languageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() { 
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        // GET: api/Category/Search?name=categoryName
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name)
        {
            var categories = await _categoryService.GetCategoryByNameAsync(name);
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            return Ok(categories);
        }

        // GET: api/Category/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);

                var categoryDto = _mapper.Map<GetAllCategoriesDTO>(category);

                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetProductsByCategoryName/{categoryName}")]
        public async Task<IActionResult> GetProductsByCategoryName(string categoryName)
        {
            try
            {
                var products = await _categoryService.GetProductsByCategoryNameAsync(categoryName);
                if (products == null || !products.Any())
                {
                    return NotFound($"No products found for category: {categoryName}");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //GET: api/Category/FilterByLanguage?languageId=2
        [HttpGet("FilterByLanguage")]
        public async Task<IActionResult> FilterByLanguage(int languageId)
        {
            IEnumerable<GetAllCategoriesDTO> categories;

            if (languageId > 0)
            {
                categories = await _categoryService.GetCategoriesByLanguageAsync(languageId);
            }
            else
            {
                categories = await _categoryService.GetAllCategoriesAsync();
            }

            return Ok(categories); 
        }
        //GET: api/Category/GetLanguages
        [HttpGet("GetLanguages")]
        public async Task<IActionResult> GetLanguages()
        {
            var languages = await _languageService.GetAllLanguagesAsync();
            return Ok(languages); 
        }
    

         [HttpGet("GetProductsByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var products = await _categoryService.GetProductsByCategoryIdAsync(categoryId);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetMainCategories")]
        public async Task<IActionResult> GetMainCategories()
        {
            try
            {
                var mainCategories = await _categoryService.GetMainCategoriesAsync();

                if (mainCategories == null || !mainCategories.Any())
                {
                    return NotFound("No main categories found.");
                }

                return Ok(mainCategories); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
