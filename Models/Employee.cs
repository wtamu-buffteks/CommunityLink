using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class Employee {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public required User user { get; set; }
        [Required]
        [StringLength(25)]
        public required string role { get; set; }
        public int hoursWorked { get; set; } = 0;

        // has many of these
        public List<ActionNeeded>? ActionNeededs { get; set; }
    }
}