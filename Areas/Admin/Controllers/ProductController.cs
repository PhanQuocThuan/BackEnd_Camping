﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackEnd_Camping.Models;
using BackEnd_Camping.Utils;
using BackEnd_Camping.Areas.Admin.DTOs.request;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing.Imaging;
namespace BackEnd_Camping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly CampingContext _context;
        private readonly IWebHostEnvironment _hostEnv;

        public ProductController(CampingContext context, IWebHostEnvironment hostEnv)
        {
            _context = context;
            _hostEnv = hostEnv;
        }

        // GET: Admin/Product
        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            IQueryable<Product> query = _context.Products;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                // Kiểm tra nếu searchQuery có thể là một số
                bool isNumeric = decimal.TryParse(searchQuery, out decimal numericValue);

                if (isNumeric)
                {
                    // Tìm kiếm theo các trường kiểu số
                    query = query.Where(p => p.Price == numericValue || p.DiscountPrice == numericValue);
                }
                else
                {
                    // Tìm kiếm theo các trường chuỗi
                    query = query.Where(p => p.Name.Contains(searchQuery) ||
                                             p.MetaKeywords.Contains(searchQuery) ||
                                             p.MetaTitle.Contains(searchQuery) ||
                                             p.MetaDescription.Contains(searchQuery));
                }
            }

            var products = await query
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .ToListAsync();
            return View(products);
        }


        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
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
            ViewData["BRA_ID"] = new SelectList(_context.Brands, "BRA_ID", "Name");
            ViewData["CAT_ID"] = new SelectList(_context.Categorys, "CAT_ID", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDTO request)
        {
            var userInfo = HttpContext.Session.Get<AdminUser>("userInfo");
            var userName = userInfo != null ? userInfo.Username : "";
            var product = new Product
            {
                PRO_ID = request.PRO_ID,
                CAT_ID = request.CAT_ID,
                BRA_ID = request.BRA_ID,
                Intro = request.Intro,
                Name = request.Name,
                Price = request.Price,
                DiscountPrice = request.DiscountPrice,
                Rate = request.Rate,
                Description = request.Description,
                Quantity = request.Quantity,
                Status = request.Status,
                MetaTitle = request.MetaTitle,
                MetaDescription = request.MetaDescription,
                MetaKeywords = request.MetaKeywords,
            };
            if (ModelState.IsValid)
            {
                string? newImageFileName = null;
                if (request.Avatar != null)
                {
                    var extension = Path.GetExtension(request.Avatar.FileName);
                    newImageFileName = $"{Guid.NewGuid().ToString()}{extension}";
                    var filePath = Path.Combine(_hostEnv.WebRootPath, "data", "products", newImageFileName);
                    request.Avatar.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if (newImageFileName != null) product.Avatar = newImageFileName;
                product.UpdatedDate = DateTime.Now;
                product.UpdatedBy = userName;
                product.CreatedDate = DateTime.Now;
                product.CreatedBy = userName;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BRA_ID"] = new SelectList(_context.Brands, "BRA_ID", "Name", product.BRA_ID);
            ViewData["CAT_ID"] = new SelectList(_context.Categorys, "CAT_ID", "Name", product.CAT_ID);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BRA_ID"] = new SelectList(_context.Brands, "BRA_ID", "Name", product.BRA_ID);
            ViewData["CAT_ID"] = new SelectList(_context.Categorys, "CAT_ID", "Name", product.CAT_ID);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductDTO request)
        {
            if (id != request.PRO_ID)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var userInfo = HttpContext.Session.Get<AdminUser>("userInfo");
                var userName = userInfo != null ? userInfo.Username : "";
                try
                {
                    product.CAT_ID = request.CAT_ID;
                    product.BRA_ID = request.BRA_ID;
                    product.Intro = request.Intro;
                    product.Name = request.Name;
                    product.Price = request.Price;
                    product.DiscountPrice = request.DiscountPrice;
                    product.Rate = request.Rate;
                    product.Description = request.Description;
                    product.Quantity = request.Quantity;
                    product.Status = request.Status;
                    product.MetaTitle = request.MetaTitle;
                    product.MetaDescription = request.MetaDescription;
                    product.MetaKeywords = request.MetaKeywords;
                    if (request.Avatar != null)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(product.Avatar))
                            {
                                var oldImagePath = Path.Combine(_hostEnv.WebRootPath, "data", "products", product.Avatar);
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
                        var directory = Path.Combine(_hostEnv.WebRootPath, "data", "products");
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        var extension = Path.GetExtension(request.Avatar.FileName);
                        var newImageFileName = $"{Guid.NewGuid().ToString()}{extension}";
                        var filePath = Path.Combine(_hostEnv.WebRootPath, "data", "products", newImageFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await request.Avatar.CopyToAsync(stream);
                        }

                        product.Avatar = newImageFileName;
                    }
                    product.UpdatedDate = DateTime.Now;
                    product.UpdatedBy = userName;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(request.PRO_ID))
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
            ViewData["BRA_ID"] = new SelectList(_context.Brands, "BRA_ID", "Name", product.BRA_ID);
            ViewData["CAT_ID"] = new SelectList(_context.Categorys, "CAT_ID", "Name", product.CAT_ID);
            return View(product);
        }

        // GET: Admin/Product/Delete/5

        // POST: Admin/Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.PRO_ID == id);
        }
    }
}
