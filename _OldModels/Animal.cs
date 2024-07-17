using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class Animal {
        [Key]
        public int AnimalID { get; set; }
        [Required]
        public int InventoryID { get; set; }
        [ForeignKey("InventoryID")]
        public required Inventory Inventory { get; set;}
        public bool Edible { get; set; } = true; //assumes edible unless stated otherwise
        public bool Perishable { get; set; } = false; //assumes nonperishable unless stated otherwise
    }
}