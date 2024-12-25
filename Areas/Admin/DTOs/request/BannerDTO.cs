
using System.ComponentModel;

namespace BackEnd_Camping.Areas.Admin.DTOs.request
{
    public class BannerDTO
    {
        public int BAN_ID { get; set; }
        public required string Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? Url { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
    }
}
