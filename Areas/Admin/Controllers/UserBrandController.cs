using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackEnd_Camping.Models;

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
            var campingContext = _context.User_brands.Include(u => u.Brand).Include(u => u.User);
            return View(await campingContext.ToListAsync());
        }

        // GET: Admin/UserBrand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBrands = await _context.User_brands
                .Include(u => u.Brand)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UBRA_ID == id);
            if (userBrands == null)
            {
                return NotFound();
            }

            return View(userBrands);
        }

        // GET: Admin/UserBrand/Create
        public IActionResult Create()
        {
            ViewData["BRA_ID"] = new SelectList(_context.Brand, "BRA_ID", "BRA_ID");
            ViewData["USE_ID"] = new SelectList(_context.User, "USE_ID", "USE_ID");
            return View();
        }

        // POST: Admin/UserBrand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UBRA_ID,USE_ID,BRA_ID,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] UserBrands userBrands)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userBrands);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BRA_ID"] = new SelectList(_context.Brand, "BRA_ID", "BRA_ID", userBrands.BRA_ID);
            ViewData["USE_ID"] = new SelectList(_context.User, "USE_ID", "USE_ID", userBrands.USE_ID);
            return View(userBrands);
        }

        // GET: Admin/UserBrand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBrands = await _context.User_brands.FindAsync(id);
            if (userBrands == null)
            {
                return NotFound();
            }
            ViewData["BRA_ID"] = new SelectList(_context.Brand, "BRA_ID", "BRA_ID", userBrands.BRA_ID);
            ViewData["USE_ID"] = new SelectList(_context.User, "USE_ID", "USE_ID", userBrands.USE_ID);
            return View(userBrands);
        }

        // POST: Admin/UserBrand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UBRA_ID,USE_ID,BRA_ID,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] UserBrands userBrands)
        {
            if (id != userBrands.UBRA_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["BRA_ID"] = new SelectList(_context.Brand, "BRA_ID", "BRA_ID", userBrands.BRA_ID);
            ViewData["USE_ID"] = new SelectList(_context.User, "USE_ID", "USE_ID", userBrands.USE_ID);
            return View(userBrands);
        }

        // GET: Admin/UserBrand/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBrands = await _context.User_brands
                .Include(u => u.Brand)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UBRA_ID == id);
            if (userBrands == null)
            {
                return NotFound();
            }

            return View(userBrands);
        }

        // POST: Admin/UserBrand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userBrands = await _context.User_brands.FindAsync(id);
            if (userBrands != null)
            {
                _context.User_brands.Remove(userBrands);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBrandsExists(int id)
        {
            return _context.User_brands.Any(e => e.UBRA_ID == id);
        }
    }
}
