using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class Request {
        [Key]
        public int RequestID { get; set; }
        [Required]
        public int RequestorID { get; set; }
        [ForeignKey("RequestorID")]
        public required Requestor Requestor { get; set; }
        public float AmountRequested { get; set; } = 0.00f;
        public float AmountRecieved { get; set; } = 0.00f;
        [Required]
        public required DateTime RequestDate { get; set; } = DateTime.Now;
        [Required]
        [StringLength(30)]
        public required string RequestTitle { get; set; }
        [Required]
        public required DateTime RequestDeadline { get; set; }
        [StringLength(3000)]
        public string? RequestDescription { get; set; }
        [StringLength(25)]
        public string? RequestStatus { get; set; }
        [StringLength(25)]
        public string? Category { get; set; }
    }
}