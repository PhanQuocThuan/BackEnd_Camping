namespace BackEnd_Camping.Areas.Admin.DTOs.response
{
    public class ViewBrand
    {
        public IEnumerable<BackEnd_Camping.Models.Brand>? Brands { get; set; } 
        public BackEnd_Camping.Models.Brand? BrandToEdit { get; set; }
        public string? SearchQuery { get; set; }
    }
}
