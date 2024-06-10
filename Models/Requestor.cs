using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class Requestor {
        [Key]
        public int RequestorID { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public required User user { get; set; }

        public List<Request> requests { get; set; } = new List<Request>();
    }
}