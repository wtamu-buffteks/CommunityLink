using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models
{
    public class Requestor
    {
        [Key]
        public int RequestorID { get; set; }

        [Required]
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public required CommunityUser User { get; set; }

        public List<Request> Requests { get; set; } = new List<Request>();
    }
}
