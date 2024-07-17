using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class Volunteer {
        [Key]
        public int VolunteerID { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public required User User { get; set;}
        public int HoursWorked { get; set; } = 0;
    }
}