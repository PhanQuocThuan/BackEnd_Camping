using BackEnd_Camping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Camping.Controllers
{
    public class ProductController : Controller
    {
        private readonly CampingContext _context;
        public ProductController(CampingContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products.AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(x => x.PRO_ID == id);
            return View(product);
        }
    }
}
