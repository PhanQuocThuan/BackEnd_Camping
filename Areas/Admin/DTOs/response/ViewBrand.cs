using BackEnd_Camping.Models;

namespace BackEnd_Camping.Areas.Admin.DTOs.response
{
    public class ViewBrand
    {
        
    }
    public class UserBrandViewModel
    {
        public User? User { get; set; }
        public int BrandCount { get; set; }
    }
    public class UserDetailViewModel
    {
        public User? User { get; set; }
        public List<Brand>? Brands { get; set; }
    }
}
