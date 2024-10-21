using Microsoft.AspNetCore.Mvc;

namespace DTOsB.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
