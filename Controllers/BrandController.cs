using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Camping.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
