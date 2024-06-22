using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class Nonedible {
        [Key]
        public int NonedibleID { get; set; }
        [Required]
        public int InventoryID { get; set; }
        [ForeignKey("InventoryID")]
        public required Inventory Inventory { get; set;}
    }
}