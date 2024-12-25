namespace BackEnd_Camping.Areas.Admin.DTOs.request
{
    public class PlaceDTO
    {
        public int PLA_ID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public IFormFile? Images { get; set; }
        public string? Description { get; set; }

    }
}
