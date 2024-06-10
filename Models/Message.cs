using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class Message {
        [Key]
        public int MessageID { get; set; }
        [Required]
        [ForeignKey("sender")]
        public int SenderID { get; set; }
        public required User sender { get; set; }
        [Required]
        [ForeignKey("receiver")]
        public int ReceiverID { get; set; }
        public required User receiver { get; set; }
        [Required]
        [StringLength(50)]
        public required string title { get; set; }
        [Required]
        [StringLength(5000)]
        public required string senderMessage { get; set; }
    }
}