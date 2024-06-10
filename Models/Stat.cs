using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class Stat {
        [Key]
        public int StatID { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public required User user { get; set; }

        public float moneyDonated { get; set; } = 0.00f; //calculated as sum of total request donations filled
        //Holds Stats of physical donations
        public List<DonationStat> donationStats { get; set; } = new List<DonationStat>();
        //Holds Stats of requests donated to
        public List<RequestStat> requestStats { get; set; } = new List<RequestStat>();
    }
}