using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class Message {
        [Key]
        public int MessageID { get; set; }
        [Required]
        public int SenderID { get; set; }
        [ForeignKey("SenderID")]
        public required User Sender { get; set; }
        [Required]
        public int ReceiverID { get; set; }
        [ForeignKey("ReceiverID")]
        public required User Receiver { get; set; }
        [Required]
        [StringLength(50)]
        public required string Title { get; set; }
        [Required]
        [StringLength(5000)]
        public required string SenderMessage { get; set; }
    }
}