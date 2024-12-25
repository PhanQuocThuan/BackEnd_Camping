using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("UserBrands")]
    public class UserBrands : BaseModel    
    {
        [Key]
        public int UBRA_ID { get; set; }
        public required int USE_ID { get; set; }
        public required int BRA_ID { get; set; }
        [ForeignKey("BRA_ID")]
        public virtual Brand? Brand { get; set; }
        [ForeignKey("USE_ID")]
        public virtual User? User { get; set; }
    }
}
