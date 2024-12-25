using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("UserCategories")]
    public class UserCategories : BaseModel
    {
        [Key]
        public int UCAT_ID { get; set; }
        public required int USE_ID { get; set; }
        public required int CAT_ID { get; set; }
        [ForeignKey("CAT_ID")]
        public virtual Category? Category { get; set; }
        [ForeignKey("USE_ID")]
        public virtual User? User { get; set; }

    }
}
