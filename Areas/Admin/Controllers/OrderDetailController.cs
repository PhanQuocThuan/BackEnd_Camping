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
    public class OrderDetailController : Controller
    {
        private readonly CampingContext _context;

        public OrderDetailController(CampingContext context)
        {
            _context = context;
        }

        // GET: Admin/OrderDetail
        public async Task<IActionResult> Index()
        {
            var campingContext = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(await campingContext.ToListAsync());
        }

        // GET: Admin/OrderDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ORDD_ID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: Admin/OrderDetail/Create
        public IActionResult Create()
        {
            ViewData["ORD_ID"] = new SelectList(_context.Orders, "ORD_ID", "ORD_ID");
            ViewData["PRO_ID"] = new SelectList(_context.Products, "PRO_ID", "Name");
            return View();
        }

        // POST: Admin/OrderDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ORDD_ID,PRO_ID,ORD_ID,Quantity,Price,DiscountPrice,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ORD_ID"] = new SelectList(_context.Orders, "ORD_ID", "ORD_ID", orderDetail.ORD_ID);
            ViewData["PRO_ID"] = new SelectList(_context.Products, "PRO_ID", "Name", orderDetail.PRO_ID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["ORD_ID"] = new SelectList(_context.Orders, "ORD_ID", "ORD_ID", orderDetail.ORD_ID);
            ViewData["PRO_ID"] = new SelectList(_context.Products, "PRO_ID", "Name", orderDetail.PRO_ID);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ORDD_ID,PRO_ID,ORD_ID,Quantity,Price,DiscountPrice,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy")] OrderDetail orderDetail)
        {
            if (id != orderDetail.ORDD_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.ORDD_ID))
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
            ViewData["ORD_ID"] = new SelectList(_context.Orders, "ORD_ID", "ORD_ID", orderDetail.ORD_ID);
            ViewData["PRO_ID"] = new SelectList(_context.Products, "PRO_ID", "Name", orderDetail.PRO_ID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ORDD_ID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: Admin/OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.ORDD_ID == id);
        }
    }
}
