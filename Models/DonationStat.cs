using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class DonationStat {
        [Key]
        public int DonationStatID { get; set; }
        [Required]
        public int StatID { get; set; }
        [ForeignKey("StatID")]
        public required Stat stat { get; set; }
        [Required]
        [StringLength(50)]
        public required string itemName { get; set; }
        [Required]
        public int quantity { get; set; } = 1;//assumes 1 was donated if no info is provided
        [Required]
        public required DateTime dateDonated { get; set; } //The day the item was donated to the organization
        public DateTime? dateGiven { get; set; } //The day the item was given to a requestor
        [Required]
        public required int RequestStatID { get; set; } //The request this item was sent to
        [ForeignKey("RequestStatID")]
        public required RequestStat requestStat { get; set; }
    }
}