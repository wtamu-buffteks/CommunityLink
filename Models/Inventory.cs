using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharityLink.Models {
    public class Inventory {
        [Key]
        public int InventoryID { get; set; }
        [Required]
        [StringLength(50)]
        public required string ItemName { get; set; }
    }
}