using BackEnd_Camping.Areas.admin.DTOs;
using BackEnd_Camping.Areas.Admin.DTOs;
using BackEnd_Camping.Models;
using BackEnd_Camping.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Camping.Controllers
{
    public class UserCustomerController : Controller
    {
        private readonly CampingContext _context;
        public UserCustomerController(CampingContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Login()
        {
            var login = Request.Cookies.Get<LoginUserDTO>("UserCustomer");
            if (login != null)
            {
                var result = await _context.Users.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.UserName == login.UserName
                            && x.Password == login.Password);
                if (result != null)
                {
                    HttpContext.Session.Set<User>("userInfo", result);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginUserDTO login)
        {
            // Tìm user dựa trên UserName
            var user = await _context.Users.AsTracking()
                .FirstOrDefaultAsync(x => x.UserName == login.UserName);

            if (user != null)
            {
                var hasher = new PasswordHasher<User>();
                // Kiểm tra mật khẩu đã mã hóa
                var passwordVerificationResult = hasher.VerifyHashedPassword(user, user.Password, login.Password);

                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    if (login.RememberMe)
                    {
                        Response.Cookies.Append<LoginUserDTO>("UserCustomer", login, new CookieOptions
                        {
                            Expires = DateTimeOffset.UtcNow.AddDays(1),
                            HttpOnly = true,
                            IsEssential = true
                        });
                    }

                    HttpContext.Session.Set<User>("userInfo", user);
                    return RedirectToAction("Index", "Home");
                }
            }

            // Sai username hoặc password
            ViewData["Message"] = "Wrong username or password";
            return View();
        }
        [HttpPost]
        public ActionResult Logout()
        {
            // Xóa thông tin trong Session
            HttpContext.Session.Remove("userInfo");

            // Xóa thông tin trong Cookies
            if (Request.Cookies["UserCustomer"] != null)
            {
                Response.Cookies.Delete("UserCustomer");
            }

            // Chuyển hướng về trang Login
            return RedirectToAction("Login");
        }
        //public async Task<ActionResult> Register(RegisterUserDTO register)
        //{
        //    var hasher = new PasswordHasher<RegisterUserDTO>();
        //    var hashedPassword = hasher.HashPassword(register, register.Password);

        //    var user = new User
        //    {
        //        UserName = register.UserName,
        //        Password = hashedPassword, 
                                           
        //    };

        //    _context.User.Add(user);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Login");
        //}
    }
}
