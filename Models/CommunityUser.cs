using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CommunityLink.Models
{
    /*
    CommunityUser set as child of IdentityUser
    leveraging the ASPCore Identity features
    properties included in the IdentityUser are not redefined
    these include `Id`, `UserName`, `Email`, `PhoneNumber`, and `PasswordHash`
    */

    /*
    ASP.Net has built in mechanism for password hashing and verification,
    so it may be better to use these in place of custom methods
    Custom methods have been included for compatibility
    */
    public class CommunityUser : IdentityUser
    {
        // Additional properties from old User class
        [PersonalData]
        [StringLength(100)]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [StringLength(25)]
        public string? UserStatus { get; set; }

        [StringLength(100)]
        public string? UserLocation { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

        // // Navigation properties
        public Volunteer? Volunteer { get; set; }
        public Employee? Employee { get; set; }
        public Requestor? Requestor { get; set; }

        // Has one of these
        public Stat? Stat { get; set; }

        // // Has many of these
        public List<Message>? MessagesSent { get; set; }
        public List<Message>? MessagesReceived { get; set; }
        public List<ActionNeeded>? ActionNeededs { get; set; }
        public List<UsersAttendingEvent> UsersAttendingEvent { get; set; } =
            new List<UsersAttendingEvent>();

        // // Methods for password handling
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
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
