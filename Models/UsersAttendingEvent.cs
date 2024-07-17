using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class UsersAttending {
        [Key, Column(Order = 0)]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public required User User { get; set; }

        [Key, Column(Order = 1)]
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public required PlannedEvent PlannedEvent { get; set; }
    }
}