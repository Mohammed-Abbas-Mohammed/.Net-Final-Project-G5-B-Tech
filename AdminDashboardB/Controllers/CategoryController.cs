using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardB.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
