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
    public class ReviewController : Controller
    {
        private readonly CampingContext _context;

        public ReviewController(CampingContext context)
        {
            _context = context;
        }

        // GET: Admin/Review
        public async Task<IActionResult> Index()
        {
            var campingContext = _context.Reviews.Include(r => r.Product).Include(r => r.User);
            return View(await campingContext.ToListAsync());
        }

        // GET: Admin/Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.REV_ID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Admin/Review/Create
        public IActionResult Create()
        {
            ViewData["PRO_ID"] = new SelectList(_context.Products, "PRO_ID", "Name");
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "USE_ID");
            return View();
        }

        // POST: Admin/Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("REV_ID,USE_ID,PRO_ID,Comment,Rate,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PRO_ID"] = new SelectList(_context.Products, "PRO_ID", "Name", review.PRO_ID);
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "USE_ID", review.USE_ID);
            return View(review);
        }

        // GET: Admin/Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["PRO_ID"] = new SelectList(_context.Products, "PRO_ID", "Name", review.PRO_ID);
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "USE_ID", review.USE_ID);
            return View(review);
        }

        // POST: Admin/Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("REV_ID,USE_ID,PRO_ID,Comment,Rate,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Review review)
        {
            if (id != review.REV_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.REV_ID))
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
            ViewData["PRO_ID"] = new SelectList(_context.Products, "PRO_ID", "Name", review.PRO_ID);
            ViewData["USE_ID"] = new SelectList(_context.Users, "USE_ID", "USE_ID", review.USE_ID);
            return View(review);
        }

        // GET: Admin/Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.REV_ID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Admin/Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.REV_ID == id);
        }
    }
}
