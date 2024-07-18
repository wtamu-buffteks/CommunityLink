using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public string? SenderID { get; set; }

        [ForeignKey("SenderID")]
        public CommunityUser? Sender { get; set; }

        [Required]
        public required string ReceiverID { get; set; }

        [ForeignKey("ReceiverID")]
        public required CommunityUser Receiver { get; set; }

        [Required]
        [StringLength(50)]
        public required string Title { get; set; }

        [Required]
        [StringLength(5000)]
        public required string SenderMessage { get; set; }
        public bool Read { get; set; } = false; // has the user read the message?
    }
}
