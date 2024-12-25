using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("Categories")]
    public class Category : BaseModel
    {
        [Key]
        public int CAT_ID { get; set; }
        [MaxLength(50, ErrorMessage = "dòng này không được quá 50 ký tự")]

        public required string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(0, 100, ErrorMessage = "DisplayOrder phải là số dương ")]

        public int DisplayOrder { get; set; }
        public bool Status { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<UserCategories>? User_Categories { get; set; }
    }
}
