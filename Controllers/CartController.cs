using BackEnd_Camping.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Camping.Controllers
{
    public class CartController : Controller
    {
        private readonly CampingContext campingContext;

        public IActionResult Index()
        {
            return View();
        }
    }
}
