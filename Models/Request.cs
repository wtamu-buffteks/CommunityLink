using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class Request {
        [Key]
        public int RequestID { get; set; }
        [Required]
        [ForeignKey("requestor")]
        public int RequestorID { get; set; }
        public Requestor requestor { get; set; }
        public float amountRequested { get; set; } = 0.00f;
        public float amountRecieved { get; set; } = 0.00f;
        public DateTime requestDate { get; set; } = Datetime.Now;
        [Required]
        [StringLength(30)]
        public string requestTitle { get; set; }
        public DateTime requestDeadline { get; set; }
        [StringLength(3000)]
        public string requestDescription { get; set; }
        [StringLength(25)]
        public string requestStatus { get; set; }
        [StringLength(25)]
        public string category { get; set; }
    }
}