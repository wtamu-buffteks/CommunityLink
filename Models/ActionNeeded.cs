using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class ActionNeeded {
        [Key]
        public int ActionNeededId { get; set; }
        [Required]
        [ForeignKey("user")]
        public int UserID { get; set; }
        public User user { get; set; }
        [ForeignKey("employee")]
        public int? EmployeeID { get; set; }
        public Employee employee { get; set; }
        [Required]
        [StringLength(50)]
        public string title { get; set; }
        [Required]
        [StringLength(5000)]
        public string actionMessage { get; set; }
    }
}