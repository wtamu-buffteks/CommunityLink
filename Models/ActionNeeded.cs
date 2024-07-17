using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class ActionNeeded {
        [Key]
        public int ActionNeededId { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public required User User { get; set; }
        public int? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee? Employee { get; set; }
        [Required]
        [StringLength(50)]
        public required string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public required string ActionMessage { get; set; }
    }
}