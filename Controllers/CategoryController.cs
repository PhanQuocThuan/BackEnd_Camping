using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Camping.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
