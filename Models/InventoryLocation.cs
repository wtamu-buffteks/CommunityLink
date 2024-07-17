using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class InventoryLocation {
        [Key]
        public int LocationID { get; set; }
        [Required]
        [StringLength(50)]
        public required string LocationName { get; set; }
        [Required]
        [StringLength(300)]
        public required string LocationAddress { get; set; }
        [Required]
        public required int Capacity { get; set; }
        public bool Cold { get; set; } = false; //does this location have refrigeration? assumes false
        public bool Frozen { get; set; } = false; //does this location have freezing? assumes false

        public List<Inventory> Inventory { get; set; } = new List<Inventory>();
        public List<InventoryPhone> InventoryPhone { get; set; } = new List<InventoryPhone>();
    }
}