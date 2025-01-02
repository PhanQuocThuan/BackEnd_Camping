using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackEnd_Camping.Models;
using BackEnd_Camping.Utils;
using BackEnd_Camping.Areas.Admin.DTOs.response;
namespace BackEnd_Camping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserBrandController : Controller
    {
        private readonly CampingContext _context;

        public UserBrandController(CampingContext context)
        {
            _context = context;
        }

        // GET: Admin/UserBrand
        public async Task<IActionResult> Index()
        {
            var campingContext = _context.UserBrands.Include(u => u.Brand).Include(u => u.User);
            return View(await campingContext.ToListAsync());
        }

        // GET: Admin/UserBrand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userBrands = await _context.UserBrands
        .Include(ub => ub.User)
        .Include(ub => ub.Brand)
        .Where(ub => ub.USE_ID == id)
        .ToListAsync();

            if (userBrands == null || !userBrands.Any())
            {
                return NotFound();
            }

            var viewModel = new UserDetailViewModel
            {
                User = userBrands.First().User,
                Brands = userBrands.Select(ub => ub.Brand).ToList()
            };

            return View(viewModel);
        }

        // GET: Admin/UserBrand/Create
        public IActionResult Create()
        {
            ViewData["BRA_ID"] = new SelectList(_context.Brands, "BRA_ID", "Name");
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "Name");
            return View();
        }

        // POST: Admin/UserBrand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserBrands userBrands)
        {
            var userInfo = HttpContext.Session.Get<AdminUser>("userInfo");
            var userName = userInfo != null ? userInfo.Username : "";
            if (ModelState.IsValid)
            {
                userBrands.CreatedBy = userName;
                userBrands.CreatedDate = DateTime.Now;
                userBrands.UpdatedDate = DateTime.Now;
                userBrands.UpdatedBy = userName;
                _context.Add(userBrands);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BRA_ID"] = new SelectList(_context.Brands, "BRA_ID", "Name", userBrands.BRA_ID);
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "Name", userBrands.USE_ID);
            return View(userBrands);
        }

        // GET: Admin/UserBrand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBrand = _context.UserBrands
        .Include(ub => ub.Brand)
        .Include(ub => ub.User)
        .FirstOrDefault(ub => ub.UBRA_ID == id);
            if (userBrand == null)
            {
                return NotFound();
            }
            ViewData["BRA_ID"] = new SelectList(_context.Brands, "BRA_ID", "Name", userBrand.BRA_ID);
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "Name", userBrand.USE_ID);
            return View(userBrand);
        }

        // POST: Admin/UserBrand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] UserBrands userBrands)
        {
            if (id != userBrands.UBRA_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userInfo = HttpContext.Session.Get<AdminUser>("userInfo");
                var userName = userInfo != null ? userInfo.Username : "";
                try
                {
                    userBrands.UpdatedDate = DateTime.Now;
                    userBrands.UpdatedBy = userName;
                    _context.Update(userBrands);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBrandsExists(userBrands.UBRA_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BRA_ID"] = new SelectList(_context.Brands, "BRA_ID", "Name", userBrands.BRA_ID);
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "Name", userBrands.USE_ID);
            return View(userBrands);
        }

        // GET: Admin/UserBrand/Delete/5

        // POST: Admin/UserBrand/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userBrands = await _context.UserBrands.FindAsync(id);
            if (userBrands != null)
            {
                _context.UserBrands.Remove(userBrands);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBrandsExists(int id)
        {
            return _context.UserBrands.Any(e => e.UBRA_ID == id);
        }
    }
    
   
}
