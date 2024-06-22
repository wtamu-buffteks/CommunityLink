using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class DonationStat {
        [Key]
        public int DonationStatID { get; set; }
        [Required]
        public int StatID { get; set; }
        [ForeignKey("StatID")]
        public required Stat Stat { get; set; }
        [Required]
        [StringLength(50)]
        public required string ItemName { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;//assumes 1 was donated if no info is provided
        [Required]
        public required DateTime DateDonated { get; set; } //The day the item was donated to the organization
        public DateTime? DateGiven { get; set; } //The day the item was given to a requestor
        [Required]
        public int RequestStatID { get; set; } //The request this item was sent to
        [ForeignKey("RequestStatID")]
        public required RequestStat RequestStat { get; set; }
    }
}