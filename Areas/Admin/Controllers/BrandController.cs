using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackEnd_Camping.Models;
using BackEnd_Camping.Utils;
using BackEnd_Camping.Areas.Admin.DTOs.request;
namespace BackEnd_Camping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly CampingContext _context;
        private readonly IWebHostEnvironment _hostEnv;

        public BrandController(CampingContext context, IWebHostEnvironment hostEnv)
        {
            _context = context;
            _hostEnv = hostEnv;
        }

        // GET: Admin/Brand
        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            var brands = string.IsNullOrEmpty(searchQuery)
         ? await _context.Brand.ToListAsync()
         : await _context.Brand
             .Where(b => b.Name.Contains(searchQuery) ||
                         b.Phone.Contains(searchQuery) ||
                         b.Address.Contains(searchQuery)||
                         b.MetaKeywords.Contains(searchQuery)||
                         b.MetaTitle.Contains(searchQuery) ||
                         b.MetaDescription.Contains(searchQuery)
                         )

             .ToListAsync();


            return View(brands);

        }

        // GET: Admin/Brand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand
                .FirstOrDefaultAsync(m => m.BRA_ID == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Admin/Brand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BrandDTO request)
        {
            var userInfo = HttpContext.Session.Get<AdminUser>("userInfo");
            var userName = userInfo != null ? userInfo.Username : "";
            var brand = new Brand
            {
                BRA_ID = request.BRA_ID,
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                Description = request.Description,
                MetaTitle = request.MetaTitle,
                MetaDescription = request.MetaDescription,
                MetaKeywords = request.MetaKeywords,
            };
            if (ModelState.IsValid)
            {
                string? newImageFileName = null;
                if (request.Avatar != null)
                {
                    var directory = Path.Combine(_hostEnv.WebRootPath, "data", "brands");
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    var extension = Path.GetExtension(request.Avatar.FileName);
                    newImageFileName = $"{Guid.NewGuid().ToString()}{extension}";
                    var filePath = Path.Combine(_hostEnv.WebRootPath, "data", "brands", newImageFileName);
                    request.Avatar.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if (newImageFileName != null) brand.Avatar = newImageFileName;
                brand.UpdatedDate = DateTime.Now;
                brand.UpdatedBy = userName;
                brand.CreatedDate = DateTime.Now;
                brand.CreatedBy = userName;
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }
        // GET: Admin/Brand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BrandDTO request)
        {
            if (id != request.BRA_ID)
            {
                return NotFound();
            }
            var brand = await _context.Brand.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var userInfo = HttpContext.Session.Get<AdminUser>("userInfo");
                var userName = userInfo != null ? userInfo.Username : "";
                try
                {
                    brand.BRA_ID = request.BRA_ID;
                    brand.Name = request.Name;
                    brand.Phone = request.Phone;
                    brand.Email = request.Email;
                    brand.Address = request.Address;
                    brand.Description = request.Description;
                    brand.MetaTitle = request.MetaTitle;
                    brand.MetaDescription = request.MetaDescription;
                    brand.MetaKeywords = request.MetaKeywords;
                    if (request.Avatar != null)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(brand.Avatar))
                            {
                                var oldImagePath = Path.Combine(_hostEnv.WebRootPath, "data", "brands", brand.Avatar);
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Không thể xóa file: {ex.Message}");
                        }
                        var directory = Path.Combine(_hostEnv.WebRootPath, "data", "brands");
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        var extension = Path.GetExtension(request.Avatar.FileName);
                        var newImageFileName = $"{Guid.NewGuid().ToString()}{extension}";
                        var filePath = Path.Combine(_hostEnv.WebRootPath, "data", "brands", newImageFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await request.Avatar.CopyToAsync(stream);
                        }

                        brand.Avatar = newImageFileName;
                    }
                    brand.UpdatedDate = DateTime.Now;
                    brand.UpdatedBy = userName;
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.BRA_ID))
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
            return View(brand);
        }

        // POST: Admin/Brand/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hasProducts = await _context.Product.AnyAsync(p => p.BRA_ID == id);
            if (hasProducts)
            {
                TempData["ErrorMessage"] = "Không thể xóa thương hiệu này vì có sản phẩm liên quan.";
                return RedirectToAction(nameof(Index));
            }

            var brand = await _context.Brand.FindAsync(id);
            if (brand != null)
            {
                _context.Brand.Remove(brand);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thương hiệu đã được xóa thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy thương hiệu.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _context.Brand.Any(e => e.BRA_ID == id);
        }
    }
}
