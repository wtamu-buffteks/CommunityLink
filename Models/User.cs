using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class User {
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(50)]
        public string username { get; set; }
        [Required]
        [StringLength(255)]
        private string passwordHash  { get; private set; }
        [StringLength(50)]
        [EmailAddress]
        public string email { get; set; }
        [StringLength(15)]
        [Phone]
        public string phoneNumber { get; set; }
        [StringLength(100)]
        public string fullName { get; set; }
        [StringLength(25)]
        public string userStatus { get; set; }
        [StringLength(100)]
        public string userLocation { get; set; }
        public DateTime startDate { get; set; } = DateTime.Now;
        public DateTime endDate { get; set; }
        //subclasses
        public Volunteer volunteer { get; set; }
        public Employee employee { get; set; }
        public Requestor requestor { get; set; }

        //has many of these
        public List<Message> messages { get; set; }
        public List<ActionNeeded> ActionNeededs { get; set; }

        //used during password creation or changing
        public void setPassword(string password)
        {
            PasswordHash = HashPassword(password);
        }

        //checks if the entered password matches the real password
        public bool validatePassword(string password)
        {
            return VerifyPassword(password, PasswordHash);
        }
        //hashes the password for secruity
        private string hashPassword(string password)
        {
            // Implement password hashing logic here
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        //checks if the password matches the real password
        private bool verifyPassword(string password, string hashedPassword)
        {
            // Implement password verification logic here
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}