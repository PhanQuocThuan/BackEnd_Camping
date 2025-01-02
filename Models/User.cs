using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd_Camping.Models
{
    [Table("Users")]
    public class User : BaseModel
    {
        [Key]
        public int USE_ID { get; set; }
        public required string Password { get; set; }
        [DisplayName("User Name")]
        public required string UserName { get; set; }
        public string? Name { get; set; }
        public bool Gender { get; set; }
        public string? Phone { get; set; }
        public required string Email { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<UserBrands>? UserBrands { get; set; }
        public virtual ICollection<UserCategories>? UserCategories { get; set; }
    }
}
