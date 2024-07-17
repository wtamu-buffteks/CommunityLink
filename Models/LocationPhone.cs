using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class InventoryPhone {
        [Key]
        public int PhoneID { get; set; }
        [StringLength(100)]
        public string? ContactName { get; set; }
        [Required]
        [StringLength(16)]
        public required string PhoneNumber { get; set; }
        public int LocationID { get; set; }
        [ForeignKey("LocationID")]
        public required InventoryLocation InventoryLocation { get; set;}
    }
}