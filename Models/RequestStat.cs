using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class RequestStat {
        [Key]
        public int RequestStatID { get; set; }
        [Required]
        public int StatID { get; set; }
        [ForeignKey("StatID")]
        public required Stat Stat { get; set; }
        [Required]
        public int OldRequestID { get; set; } //made to account for the fact that some requestors might give the same title to the project, it is not a forign key because the original request will only exsist for as long as it's needed after expiration
        [Required]
        [StringLength(50)]
        public required string RequestorUsername { get; set; } //Wasn't sure if we should tie it to the name, so I set it to the Username.
        public float AmountDonated { get; set; } = 0.00f; //holds all, if any, money donated to the request
        [Required]
        public required DateTime DonationDate { get; set; } //The day the donation was made
        [Required]
        [StringLength(30)]
        public required string RequestTitle { get; set; }
        public List<DonationStat> DonationStats { get; set; } = new List<DonationStat>(); //holds all the physical items that may have been donated to this request
    }
}