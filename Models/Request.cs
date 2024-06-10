using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class Request {
        [Key]
        public int RequestID { get; set; }
        [Required]
        public int RequestorID { get; set; }
        [ForeignKey("RequestorID")]
        public required Requestor requestor { get; set; }
        public float amountRequested { get; set; } = 0.00f;
        public float amountRecieved { get; set; } = 0.00f;
        [Required]
        public required DateTime requestDate { get; set; } = DateTime.Now;
        [Required]
        [StringLength(30)]
        public required string requestTitle { get; set; }
        [Required]
        public required DateTime requestDeadline { get; set; }
        [StringLength(3000)]
        public string? requestDescription { get; set; }
        [StringLength(25)]
        public string? requestStatus { get; set; }
        [StringLength(25)]
        public string? category { get; set; }
    }
}