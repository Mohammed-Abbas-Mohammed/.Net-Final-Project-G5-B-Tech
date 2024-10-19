using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardB.Controllers
{
    public class UserController : Controller
    {
        public async Task<IActionResult> Index()
        {


            return View();
        }

        public async Task<IActionResult> Edit()
        {

            return View();

        }

        //[HttpPost]
        //public async Task<IActionResult> Edit( )
        //{


        //    return RedirectToAction("Index");

        //}
        public IActionResult LockUnlock(string? id)
        {
            return RedirectToAction();
        }
    }
}
