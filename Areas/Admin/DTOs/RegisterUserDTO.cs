namespace BackEnd_Camping.Areas.Admin.DTOs
{
    public class RegisterUserDTO
    {
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }
    }
}
