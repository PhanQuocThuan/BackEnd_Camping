using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackEnd_Camping.Models;
using BackEnd_Camping.Utils;
namespace BackEnd_Camping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserCategorieController : Controller
    {
        private readonly CampingContext _context;

        public UserCategorieController(CampingContext context)
        {
            _context = context;
        }

        // GET: Admin/UserCategorie
        public async Task<IActionResult> Index()
        {
            var campingContext = _context.UserCategories.Include(u => u.Category).Include(u => u.User);
            return View(await campingContext.ToListAsync());
        }

        // GET: Admin/UserCategorie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCategories = await _context.UserCategories
                .Include(u => u.Category)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UCAT_ID == id);
            if (userCategories == null)
            {
                return NotFound();
            }

            return View(userCategories);
        }

        // GET: Admin/UserCategorie/Create
        public IActionResult Create()
        {
            ViewData["CAT_ID"] = new SelectList(_context.Categorys, "CAT_ID", "Name");
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "Name");
            return View();
        }

        // POST: Admin/UserCategorie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserCategories userCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CAT_ID"] = new SelectList(_context.Categorys, "CAT_ID", "Name", userCategories.CAT_ID);
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "Name", userCategories.USE_ID);
            return View(userCategories);
        }

        // GET: Admin/UserCategorie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCategories = await _context.UserCategories.FindAsync(id);
            if (userCategories == null)
            {
                return NotFound();
            }
            ViewData["CAT_ID"] = new SelectList(_context.Categorys, "CAT_ID", "Name", userCategories.CAT_ID);
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "Name", userCategories.USE_ID);
            return View(userCategories);
        }

        // POST: Admin/UserCategorie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] UserCategories userCategories)
        {
            if (id != userCategories.UCAT_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCategoriesExists(userCategories.UCAT_ID))
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
            ViewData["CAT_ID"] = new SelectList(_context.Categorys, "CAT_ID", "Name", userCategories.CAT_ID);
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "Name", userCategories.USE_ID);
            return View(userCategories);
        }

        // GET: Admin/UserCategorie/Delete/5

        // POST: Admin/UserCategorie/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userCategories = await _context.UserCategories.FindAsync(id);
            if (userCategories != null)
            {
                _context.UserCategories.Remove(userCategories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCategoriesExists(int id)
        {
            return _context.UserCategories.Any(e => e.UCAT_ID == id);
        }
    }
}
