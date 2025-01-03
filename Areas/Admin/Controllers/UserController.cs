﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd_Camping.Areas.admin.DTOs;
using BackEnd_Camping.Models;
using BackEnd_Camping.Utils;

namespace BackEnd_Camping.Areas.admin.Controllers
{
    [Area("admin")]
    public class UserController : Controller
    {
        private readonly CampingContext _context;
        public UserController(CampingContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Login()
        {
            var login = Request.Cookies.Get<LoginDTO>("UserCredential");
            if (login != null)
            {
                var result = await _context.AdminUsers.AsTracking()
                            .FirstOrDefaultAsync(x => x.Username == login.UserName
                            && x.Password == login.Password);
                if (result != null)
                {
                    HttpContext.Session.Set<AdminUser>("userInfo", result);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            var result = await _context.AdminUsers.AsTracking()
                .FirstOrDefaultAsync(x => x.Username == login.UserName
                && x.Password == login.Password);
            if (result != null)
            {
                if (login.RememberMe)
                {
                    Response.Cookies.Append<LoginDTO>("UserCredential", login, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(1),
                        HttpOnly = true,
                        IsEssential = true
                    });
                }
                HttpContext.Session.Set<AdminUser>("userInfo", result);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "Wrong username or password";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Logout()
        {
            // Xóa thông tin trong Session
            HttpContext.Session.Remove("userInfo");

            // Xóa thông tin trong Cookies
            if (Request.Cookies["UserCredential"] != null)
            {
                Response.Cookies.Delete("UserCredential");
            }

            // Chuyển hướng về trang Login
            return RedirectToAction("Login");
        }

    }
}
