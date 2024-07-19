using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models
{
    public class Stat
    {
        [Key]
        public int StatID { get; set; }

        [Required]
        public string UserID { get; set; } = default!;

        [ForeignKey("UserID")]
        public required CommunityUser User { get; set; }

        public float MoneyDonated { get; set; } = 0.00f; //calculated as sum of total request donations filled

        //Holds Stats of physical donations
        public List<DonationStat> DonationStats { get; set; } = new List<DonationStat>();

        //Holds Stats of requests donated to
        public List<RequestStat> RequestStats { get; set; } = new List<RequestStat>();
    }
}
