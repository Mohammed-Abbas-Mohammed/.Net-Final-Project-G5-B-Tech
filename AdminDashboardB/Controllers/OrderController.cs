using Microsoft.AspNetCore.Mvc;

namespace DTOsB.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
