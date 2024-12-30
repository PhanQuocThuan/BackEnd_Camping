namespace BackEnd_Camping.Areas.Admin.DTOs
{
    public class LoginUserDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
