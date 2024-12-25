using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BackEnd_Camping.Models
{
    [Table("AdminUsers")]
    public class AdminUser : BaseModel
    {
        [Key]
        public int ADU_ID { get; set; }

        [DisplayName("User Name")]
        [MaxLength(100, ErrorMessage = "UserName không được quá 50 ký tự")]
        public required string Username { get; set; }
        public required string Password { get; set; }
        [DisplayName("Display Name")]
        [MaxLength(100, ErrorMessage = "dòng này không được quá 50 ký tự")]
        public string? Email { get; set; }
        public bool Role { get; set; }
    }
}
