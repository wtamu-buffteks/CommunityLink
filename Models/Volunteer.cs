using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class Volunteer {
        [Key]
        public int VolunteerID { get; set; }
        [Required]
        [ForeignKey("user")]
        public int UserID { get; set; }
        public required User user { get; set;}
        public int hoursWorked { get; set; } = 0;
    }
}