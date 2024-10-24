using ApplicationB.Contracts_B.Category;
using ApplicationB.Services_B.Category;
using ApplicationB.Services_B.General;
using AutoMapper;
using DTOsB.Category;
using InfrastructureB.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminDashboardB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, ILanguageService languageService, IMapper mapper) { 
            _categoryService =categoryService;
            _languageService = languageService;
            _mapper= mapper;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return View(categories);
        }
        public async Task<IActionResult> Search(string name)
        {
            var categories = await _categoryService.GetCategoryByNameAsync(name);
            return View("Index", categories);
        }
        public async Task<IActionResult> Create()
        {
            // Get all languages for the dropdown list
            var languages = await _languageService.GetAllLanguagesAsync();
            ViewBag.Languages = languages.Select(l => new SelectListItem
            {
                
                Value = l.Id.ToString(), 
                Text = l.Name
            }).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrUpdateCategoriesDTO model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Translations[0].IsMainCategory == null)
                    {
                        model.Translations[0].IsMainCategory = false;
                    }
                    await _categoryService.AddCategoryAsync(model, imageFile);
                    return RedirectToAction("Index", "Category"); // Redirect to the category list
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    var errors = entry.Value.Errors;
                    foreach (var error in errors)
                    {
                       
                        Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                    }
                }
            }
            // Reload the languages if the model state is invalid
            var languages = await _languageService.GetAllLanguagesAsync();
            ViewBag.Languages = languages.Select(l => new SelectListItem
            {
                
                Value = l.Id.ToString(), 
                Text = l.Name
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<CreateOrUpdateCategoriesDTO>(category);

            if (model.Translations == null || !model.Translations.Any())
            {
                model.Translations = new List<CreateCategoryTranslationDto>
                {
                   new CreateCategoryTranslationDto()
                };
            }

            var languages = await _languageService.GetAllLanguagesAsync();
            if (languages == null || !languages.Any())
            {
                ModelState.AddModelError("", "No languages found.");
            }
            ViewBag.Languages = new SelectList(languages, "Id", "Name");


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateOrUpdateCategoriesDTO model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategoryAsync(id, model, imageFile);
                    return RedirectToAction("Index", "Category");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating category: {ex.Message}");

                    ModelState.AddModelError("", ex.Message);
                }
            }

            var languages = await _languageService.GetAllLanguagesAsync();
            ViewBag.Languages = languages.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            }).ToList();

            return View(model);
        }

        // GET: Category/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var categoryEntity = await _categoryService.GetCategoryByIdAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }
            var categoryDto = _mapper.Map<GetAllCategoriesDTO>(categoryEntity);
            return View(categoryDto);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

