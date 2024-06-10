using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class ActionNeeded {
        [Key]
        public int ActionNeededId { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public required User user { get; set; }
        public int? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee? employee { get; set; }
        [Required]
        [StringLength(50)]
        public required string title { get; set; }
        [Required]
        [StringLength(5000)]
        public required string actionMessage { get; set; }
    }
}