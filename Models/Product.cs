using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("Products")]
    public class Product : BaseModel
    {
        [Key]
        public int PRO_ID { get; set; }
        public required int CAT_ID { get; set; }
        public required int BRA_ID { get; set; }
        public string? Avatar { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        [MaxLength(20,ErrorMessage = "Tên sản phẩm không được quá 50 ký tự")]
        public required string Name { get; set; }
		[Required(ErrorMessage = "Giá sản phẩm là bắt buộc.")]
		[Range(0, double.MaxValue,ErrorMessage ="giá sản phẩm phải là số dương ")]
        [MaxLength(10, ErrorMessage = "Giá sản phẩm không được quá 10 ký tự")]
        public required decimal Price { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "giá sản phẩm phải là số dương ")]
        [DisplayName("Discount Price")]
        [MaxLength(10, ErrorMessage = "Giá sản phẩm không được quá 10 ký tự")]
        public decimal? DiscountPrice { get; set; }
        public double? Rate { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public bool Status { get; set; }
        public string? Intro { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        [ForeignKey("CAT_ID")]
        public virtual Category? Category { get; set; }
        [ForeignKey("BRA_ID")]
        public virtual Brand? Brand { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
