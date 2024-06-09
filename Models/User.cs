using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class User {
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        private string UserPassword { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(25)]
        public string UserStatus { get; set; }
        [StringLength(100)]
        public string UserLocation { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public void SetPassword(string password)
        {
            PasswordHash = HashPassword(password);
        }

        public bool ValidatePassword(string password)
        {
            return VerifyPassword(password, PasswordHash);
        }

        private string HashPassword(string password)
        {
            // Implement password hashing logic here
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // Implement password verification logic here
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}