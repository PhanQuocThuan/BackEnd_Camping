using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("Brands")]
    public class Brand : BaseModel
    {
        [Key]
        public int BRA_ID { get; set; }
        [MaxLength(50, ErrorMessage = "dòng này không được quá 50 ký tự")]
        public string? Name { get; set; }
        public string? Avatar {  get; set; }
        [MaxLength(10, ErrorMessage = "dòng này không được quá 50 ký tự")]
        public string? Phone { get; set; }
        [MaxLength(50, ErrorMessage = "dòng này không được quá 50 ký tự")]

        public string? Email { get; set; }
        [MaxLength(50, ErrorMessage = "dòng này không được quá 50 ký tự")]
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<UserBrands>? User_Brands { get; set; }
    }
}
