using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models
{
    public class Volunteer
    {
        [Key]
        public int VolunteerID { get; set; }

        [Required]
        public required string UserID { get; set; }

        [ForeignKey("UserID")]
        public required CommunityUser User { get; set; }
        public int HoursWorked { get; set; } = 0;
    }
}
