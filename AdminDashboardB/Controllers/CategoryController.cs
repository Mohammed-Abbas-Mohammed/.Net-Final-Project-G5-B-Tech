using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardB.Controllers
{
    public class CategoryController : Controller
    {
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
            return NotFound();

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
