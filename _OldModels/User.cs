using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class User {
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(50)]
        public required string Username { get; set; }
        private string _PasswordHash = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash
        {
            get => _PasswordHash;
            private set => _PasswordHash = value;  // Private setter
        }
        [StringLength(50)]
        [EmailAddress]
        public string? Email { get; set; }
        [StringLength(15)]
        [Phone]
        public string? PhoneNumber { get; set; }
        [StringLength(100)]
        public string? FullName { get; set; }
        [StringLength(25)]
        public string? UserStatus { get; set; }
        [StringLength(100)]
        public string? UserLocation { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        //subclasses
        public Volunteer? Volunteer { get; set; }
        public Employee? Employee { get; set; }
        public Requestor? Requestor { get; set; }
        // has one of these
        public Stat? Stat { get; set; }

        //has many of these
        public List<Message>? MessagesSent  { get; set; }
        public List<Message>? MessagesReceived { get; set; }
        public List<ActionNeeded>? ActionNeededs { get; set; }
         public List<UsersAttending> UsersAttending { get; set; } = new List<UsersAttending>();

        //used during password creation or changing
        public void SetPassword(string password)
        {
            PasswordHash = HashPassword(password);
        }

        //checks if the entered password matches the real password
        public bool ValidatePassword(string password)
        {
            return VerifyPassword(password, PasswordHash);
        }
        //hashes the password for secruity
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        //checks if the password matches the real password
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}