using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models
{
    public class UsersAttendingEvent
    {
        // Old way
        // [Key, Column(Order = 0)]
        // public int UserID { get; set; }

        // // [ForeignKey("UserID")]
        // // public required User User { get; set; }

        // [Key, Column(Order = 1)]
        // public int EventID { get; set; }

        // [ForeignKey("EventID")]
        // public required PlannedEvent PlannedEvent { get; set; }
        public required string UserID { get; set; }

        [ForeignKey("UserID")]
        public required CommunityUser User { get; set; }

        public int EventID { get; set; }

        [ForeignKey("EventID")]
        public required PlannedEvent PlannedEvent { get; set; }
    }
}
