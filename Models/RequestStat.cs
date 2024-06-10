using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class RequestStat {
        [Key]
        public int RequestStatID { get; set; }
        [Required]
        public int StatID { get; set; }
        [ForeignKey("StatID")]
        public required Stat stat { get; set; }
        [Required]
        [StringLength(50)]
        public required string requestorUsername { get; set; } //Wasn't sure if we should tie it to the name, so I set it to the Username.
        public float amountDonated { get; set; } = 0.00f; //holds all, if any, money donated to the request
        [Required]
        public required DateTime donationDate { get; set; } //The day the donation was made
        [Required]
        [StringLength(30)]
        public required string requestTitle { get; set; }

        public List<DonationStat> donationStats { get; set; } = new List<DonationStat>(); //holds all the physical items that may have been donated to this request
    }
}