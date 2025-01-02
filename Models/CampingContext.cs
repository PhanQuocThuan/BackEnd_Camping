using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BackEnd_Camping.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackEnd_Camping.Models
{
    public class CampingContext : IdentityDbContext
    {
        public CampingContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<BackEnd_Camping.Models.Banner> Banners { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Brand> Brands { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Category> Categorys { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Contact> Contacts { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Feature> Features { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Menu> Menus { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.User> Users { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Product> Products { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Order> Orders { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.OrderDetail> OrderDetails { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Place> Places { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Review> Reviews { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Setting> Settings { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.UserBrands> UserBrands { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.UserCategories> UserCategories { get; set; } = default!;
        //public DbSet<Banner> Banners { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Feature> Features { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Review> Reviews { get; set; }
        //public DbSet<Setting> Settings { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Brand> Brands { get; set; }
        //public DbSet<Place> Places { get; set; }
        //public DbSet<Menu> Menus { get; set; }
        //public DbSet<UserBrands> UserBrands { get; set; }
        //public DbSet<UserCategories> UserCategories { get; set; }
    }
}
