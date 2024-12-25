using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("Orders")]
    public class Order : BaseModel
    {
        [Key]
        public int ORD_ID { get; set; }
        public required int USE_ID { get; set; }
        [DisplayName("Order Date")]
        public required DateTime OrderDate { get; set; }
        [DisplayName("Total Amount")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá sản phẩm không được âm")]
        public decimal TotalAmount { get; set; }
        public string? ShippingAddress { get; set; }
        public string? OrderStatus { get; set; }
        [DisplayName("Payment Status")]
        public string? PaymentStatus { get; set; }
        [MaxLength(50, ErrorMessage = "Note không được quá 50 ký tự")]
        [ForeignKey("USE_ID")]
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
