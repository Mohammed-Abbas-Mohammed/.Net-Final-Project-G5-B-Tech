using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardB.Controllers
{
    public class OrderController : Controller
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

        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> Details(int orderid)
        {
            return View();
        }

        public async Task<IActionResult> CancelOrder()
        {
            return RedirectToAction();
        }
        public async Task<IActionResult> UpdateOrderDetails()
        {
            return RedirectToAction();
        }
        public async Task<IActionResult> StartProccess()
        {
            return RedirectToAction();
        }

        public async Task<IActionResult> StartShip()
        {
            return RedirectToAction();
        }
    }
}
