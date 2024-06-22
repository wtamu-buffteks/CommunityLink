using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class Edible {
        [Key]
        public int EdibleID { get; set; }
        [Required]
        public int InventoryID { get; set; }
        [ForeignKey("InventoryID")]
        public required Inventory Inventory { get; set;}
        public bool Perishable { get; set; } = false; //assumes nonperishable unless stated otherwise
    }
}