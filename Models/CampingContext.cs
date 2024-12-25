using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BackEnd_Camping.Models;

namespace BackEnd_Camping.Models
{
    public class CampingContext : DbContext
    {
        public CampingContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<BackEnd_Camping.Models.Banner> Banner { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Brand> Brand { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Category> Category { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Contact> Contact { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Feature> Feature { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Menu> Menu { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.User> User { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Product> Product { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Order> Order { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Place> Place { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Review> Review { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.Setting> Setting { get; set; } = default!;
        public DbSet<BackEnd_Camping.Models.UserBrands> User_brands { get; set; } = default!;
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
        //public DbSet<User_brands> user_Brands { get; set; }
        //public DbSet<User_categories> user_categories { get; set; }
    }
}
