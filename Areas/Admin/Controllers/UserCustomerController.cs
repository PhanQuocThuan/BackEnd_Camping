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
    public class UserCustomerController : Controller
    {
        private readonly CampingContext _context;

        public UserCustomerController(CampingContext context)
        {
            _context = context;
        }

        // GET: Admin/UserCustomer
        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            var users = string.IsNullOrEmpty(searchQuery)
       ? await _context.Users.Include(u => u.UserBrands).ThenInclude(ub => ub.Brand).ToListAsync()
       : await _context.Users
       .Include(u => u.UserBrands).ThenInclude(ub => ub.Brand)
       .Include(u => u.UserCategories).ThenInclude(ub => ub.Category)
           .Where(b => b.Name.Contains(searchQuery) ||
                       b.UserName.Contains(searchQuery) ||
                       b.Email.Contains(searchQuery) ||
                       b.Phone.Contains(searchQuery)
                       )

           .ToListAsync();


            return View(users);
        }

        // GET: Admin/UserCustomer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.USE_ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/UserCustomer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserCustomer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] User user)
        {
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email);
            bool userNameExists = await _context.Users.AnyAsync(u => u.UserName == user.UserName);

            if (emailExists || userNameExists)
            {
                if (emailExists)
                {
                    ModelState.AddModelError("Email", "Email này đã được sử dụng.");
                }
                if (userNameExists)
                {
                    ModelState.AddModelError("UserName", "Tên người dùng này đã được sử dụng.");
                }
                return View(user); // Trả lại dữ liệu nhập kèm lỗi
            }
            if (ModelState.IsValid)
            {
                var userInfo = HttpContext.Session.Get<AdminUser>("userInfo");
                var userName = userInfo != null ? userInfo.Username : "";

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                user.UpdatedDate = DateTime.Now;
                user.UpdatedBy = userName;
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = userName;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Admin/UserCustomer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/UserCustomer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] User user, [FromForm] string OldPassword)
        {
            if (id != user.USE_ID)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                // Giữ mật khẩu cũ nếu không thay đổi
                user.Password = OldPassword;
            }
            else
            {
                // Mã hóa mật khẩu mới nếu có thay đổi
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var userInfo = HttpContext.Session.Get<AdminUser>("userInfo");
                    var userName = userInfo != null ? userInfo.Username : "";
                    user.UpdatedDate = DateTime.Now;
                    user.UpdatedBy = userName;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.USE_ID))
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
            return View(user);
        }

        // GET: Admin/UserCustomer/Delete/5

        // POST: Admin/UserCustomer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hasProducts = await _context.Orders.AnyAsync(p => p.USE_ID == id);
            if (hasProducts)
            {
                TempData["ErrorMessage"] = "Không thể xóa người dùng này vì có đơn hàng hoặc yêu thích liên quan.";
                return RedirectToAction(nameof(Index));
            }

            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Người dùng đã được xóa thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy Người dùng.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.USE_ID == id);
        }
        // GET: Admin/UserCustomer/Favorites/5
        public async Task<IActionResult> Favorites(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Lấy danh sách thương hiệu yêu thích của người dùng
            var favoriteBrands = await _context.UserBrands
                .Where(uf => uf.USE_ID == id)
                .Include(uf => uf.Brand) // Include để lấy thông tin Brand
                .Select(uf => new Brand
                {
                    BRA_ID = uf.Brand.BRA_ID,
                    Name = uf.Brand.Name,
                    Description = uf.Brand.Description
                })
                .ToListAsync();

            if (favoriteBrands == null || !favoriteBrands.Any())
            {
                TempData["ErrorMessage"] = "Người dùng này không có thương hiệu yêu thích nào.";
                return RedirectToAction(nameof(Index));
            }

            // Gán tên người dùng vào ViewData để hiển thị
            ViewData["UserName"] = await _context.Users
                .Where(u => u.USE_ID == id)
                .Select(u => u.Name)
                .FirstOrDefaultAsync();

            return View("~/Areas/Admin/Views/UserCustomer/Favorites.cshtml", favoriteBrands);

        }
        public async Task<IActionResult> FavoriteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Lấy danh sách thương hiệu yêu thích của người dùng
            var favoriteCategories = await _context.UserCategories
                .Where(uf => uf.USE_ID == id)
                .Include(uf => uf.Category) // Include để lấy thông tin Brand
                .Select(uf => new Brand
                {
                    BRA_ID = uf.Category.CAT_ID,
                    Name = uf.Category.Name,
                })
                .ToListAsync();

            if (favoriteCategories == null || !favoriteCategories.Any())
            {
                TempData["ErrorMessage"] = "Người dùng này không có thương hiệu yêu thích nào.";
                return RedirectToAction(nameof(Index));
            }

            // Gán tên người dùng vào ViewData để hiển thị
            ViewData["UserName"] = await _context.Users
                .Where(u => u.USE_ID == id)
                .Select(u => u.Name)
                .FirstOrDefaultAsync();

            return View("~/Areas/Admin/Views/UserCustomer/FavoriteCategory.cshtml", favoriteCategories);

        }

    }
}
