using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class PlannedEvent {
        [Key]
        public int EventID { get; set; }
        public int RSVP { get; set; } = 0;//holds all who RSVP including those who don't have an account
        [Required]
        [StringLength(50)]
        public required string EventName { get; set; }
        [Required]
        [StringLength(5000)]
        public required string EventDescription { get; set; }
        [Required]
        public required DateTime EventDate { get; set; }
        [StringLength(300)]
        public string? EventLocation { get; set; }

        //any inventory at location
        public List<Inventory> Inventory { get; set; } = new List<Inventory>();
        //any users who RSVPd
        public List<UsersAttending> UsersAttending { get; set; } = new List<UsersAttending>();
    }
}