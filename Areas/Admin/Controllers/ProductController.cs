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
    public class ProductController : Controller
    {
        private readonly CampingContext _context;

        public ProductController(CampingContext context)
        {
            _context = context;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            var campingContext = _context.Product.Include(p => p.Brand).Include(p => p.Category);
            return View(await campingContext.ToListAsync());
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PRO_ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            ViewData["BRA_ID"] = new SelectList(_context.Brand, "BRA_ID", "BRA_ID");
            ViewData["CAT_ID"] = new SelectList(_context.Category, "CAT_ID", "CAT_ID");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PRO_ID,CAT_ID,BRA_ID,Avatar,Name,Price,DiscountPrice,Rate,Description,Quantity,Status,Intro,MetaTitle,MetaDescription,MetaKeywords,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BRA_ID"] = new SelectList(_context.Brand, "BRA_ID", "BRA_ID", product.BRA_ID);
            ViewData["CAT_ID"] = new SelectList(_context.Category, "CAT_ID", "CAT_ID", product.CAT_ID);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BRA_ID"] = new SelectList(_context.Brand, "BRA_ID", "BRA_ID", product.BRA_ID);
            ViewData["CAT_ID"] = new SelectList(_context.Category, "CAT_ID", "CAT_ID", product.CAT_ID);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PRO_ID,CAT_ID,BRA_ID,Avatar,Name,Price,DiscountPrice,Rate,Description,Quantity,Status,Intro,MetaTitle,MetaDescription,MetaKeywords,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] Product product)
        {
            if (id != product.PRO_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.PRO_ID))
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
            ViewData["BRA_ID"] = new SelectList(_context.Brand, "BRA_ID", "BRA_ID", product.BRA_ID);
            ViewData["CAT_ID"] = new SelectList(_context.Category, "CAT_ID", "CAT_ID", product.CAT_ID);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PRO_ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.PRO_ID == id);
        }
    }
}
