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
        [Required(ErrorMessage = "Tên không được để trống")]
        public required string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(0, int.MaxValue, ErrorMessage = "Thứ tự hiển thị phải là số dương không vượt quá 2,147,483,647")]
        public int DisplayOrder { get; set; }
        public bool Status { get; set; }
        [DisplayName("Meta Title")]
        public string? MetaTitle { get; set; }
        [DisplayName("Meta Description")]
        public string? MetaDescription { get; set; }
        [DisplayName("Meta Keywords")]
        public string? MetaKeywords { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<UserCategories>? User_Categories { get; set; }
    }
}
