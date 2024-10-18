using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardB.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Details()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Create()
        //{

        //    return NotFound();

        //}
        public async Task<IActionResult> Edit()
        {

            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(int id)
        //{

        //    return NotFound();

        //}

        public async Task<IActionResult> Search(string name)
        {

            return NotFound();

        }



        public async Task<IActionResult> Delete(int id)
        {

            return NotFound();

        }
    }
}
