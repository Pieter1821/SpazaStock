namespace SpazaStock.Server.DTOs
{
    public class RegisterDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string MobileNumber { get; set; }
        public required string Password { get; set; }
    }
}
