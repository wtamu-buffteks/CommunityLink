using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class Requestor {
        [Key]
        public int RequestorID { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public required User User { get; set; }

        public List<Request> Requests { get; set; } = new List<Request>();
    }
}