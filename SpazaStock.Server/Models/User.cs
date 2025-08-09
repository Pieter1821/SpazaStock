using System;

namespace SpazaStock.Server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty; // +27 format
        public string PasswordHash { get; set; } = string.Empty;
        public bool SmsEnabled { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
