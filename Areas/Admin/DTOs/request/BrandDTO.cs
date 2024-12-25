using BackEnd_Camping.Models;

namespace BackEnd_Camping.Areas.Admin.DTOs.request
{
    public class BrandDTO
    {
        public int BRA_ID { get; set; }
        public string? Name { get; set; }
        public IFormFile? Avatar { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
    }
}
