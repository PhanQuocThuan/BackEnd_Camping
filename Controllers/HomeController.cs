using BackEnd_Camping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BackEnd_Camping.Controllers
{
    public class HomeController : Controller
    {
        private readonly CampingContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CampingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Features"] = _context.Features.AsNoTracking().ToList();

            ViewData["Brands"] = _context.Brands.AsNoTracking()
                 .Include(x => x.Products.OrderBy(y => y.Name))
            .OrderBy(x => x.BRA_ID).ToList();

            ViewData["Categories"] = _context.Categorys.AsNoTracking()
                 .Include(x => x.Products.OrderBy(y => y.Name))
                 .Where(x => x.Status == false)
                 .OrderBy(x => x.CAT_ID)
                 .ToList();

            ViewData["HostProducts"] = _context.Products.AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .OrderBy(x => x.Price).ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
