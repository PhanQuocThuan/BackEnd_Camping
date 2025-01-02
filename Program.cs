using Microsoft.EntityFrameworkCore;
using BackEnd_Camping.Models;


namespace BackEnd_Camping
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<CampingContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                //options.Cookie.Name = ".AspNetCore.Session";  // Set Name key on browser
                options.IdleTimeout = TimeSpan.FromHours(1); // Set session timeout
                options.Cookie.HttpOnly = true;         // Ensures the session cookie is accessible only by the server
                options.Cookie.IsEssential = true;      // Required for GDPR compliance
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            //app.UseMiddleware<BackEnd_Camping.Middleware.AdminAccessMiddleware>();
            

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
             );//route areas ph?i ?? tr??c

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
