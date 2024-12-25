using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("Features")]
    public class Feature : BaseModel
    {
        [Key]
        public int FEA_ID { get; set; }
        public string? Icon { get; set; }
        [MaxLength(50, ErrorMessage = "dòng này không được quá 50 ký tự")]

        public required string Title { get; set; }
        [DisplayName("Sub Title")]
        [MaxLength(50, ErrorMessage = "dòng này không được quá 50 ký tự")]
        public string? Subtitle { get; set; }
        [DisplayName("Display Order")]
        
        public int DisplayOrder { get; set; }
    }
}
