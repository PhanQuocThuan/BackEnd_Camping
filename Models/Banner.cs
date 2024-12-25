using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("Banners")]
    public class Banner : BaseModel
    {
        [Key]
        public int BAN_ID { get; set; }
        [MaxLength(50, ErrorMessage = "dòng này không được quá 50 ký tự")]  
        public required string Title { get; set; }
        public string? Image { get; set; }
        public string? Url { get; set; }
        [DisplayName("Display Order")]
        [Range(0,100, ErrorMessage = "DisplayOrder phải là số dương ")]
        public int DisplayOrder { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
    }
}
