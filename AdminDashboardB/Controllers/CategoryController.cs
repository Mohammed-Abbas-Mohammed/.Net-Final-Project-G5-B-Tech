
﻿using ApplicationB.Services_B.Category.NewFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelsB.Authentication_and_Authorization_B;


namespace AdminDashboardB.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly UserManager<ApplicationUserB> _userManager;

        public CategoryController(UserManager<ApplicationUserB> userManager, ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> GetAll()
        {
            var categories =await _categoryService.GetAllCategoriesAsync();
            return View(categories);

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateCategoryDTO category)
        //{
        //    return NotFound();
        //}

        public async Task<IActionResult> Delete(int id)
        {
            return NotFound();
        }


        public async Task<IActionResult> Edit(int id)
        {
            return View();

        }

        //[HttpPost]
        //public async Task<IActionResult> Edit( )
        //{

        //    return NotFound();

        //}

        public async Task<IActionResult> Search(string name)
        {

            return NotFound();

        }
    }
}
