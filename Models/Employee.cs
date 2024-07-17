using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class Employee {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public required User User { get; set; }
        [Required]
        [StringLength(25)]
        public required string Role { get; set; }
        public int HoursWorked { get; set; } = 0;

        // has many of these
        public List<ActionNeeded>? ActionNeededs { get; set; }
    }
}