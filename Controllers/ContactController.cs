using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Camping.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
