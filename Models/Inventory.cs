using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLink.Models {
    public class Inventory {
        [Key]
        public int InventoryID { get; set; }
        [Required]
        [StringLength(50)]
        public required string ItemName { get; set; }
        [StringLength(1000)]
        public string? ItemDescription { get; set; }
        public int Quantity { get; set; } = 1;//Not total quantity, quantity in each instance of this item ex: 6 for a pack of soda containing 6 cans
        [StringLength(10)]
        public string? UnitOfMeasurement { get; set; } = "unit";
        public float UnitCost { get; set; } = 0.00f;//used if the organization purchases inventory, will be $0.00 otherwise
        [Required]
        public required DateTime DateReceived { get; set; } = DateTime.Now;//holds the day the organization recieved the item
        [Required]
        [StringLength(100)]
        public required string Barcode { get; set; }
        [StringLength(25)]
        public string StorageType { get; set; } = "Standard";
        public DateTime? ExpirationDate { get; set; }
        //located in storage or at an event
        public int LocationID { get; set; }
        [ForeignKey("LocationID")]
        public required InventoryLocation InventoryLocation { get; set;}
        
        public int EventID { get; set; }
        [ForeignKey("EventID")]
        public required PlannedEvent PlannedEvent { get; set;}

        //subclasses
        public Nonedible? Nonedible { get; set; }
        public Edible? Edible { get; set; }
        public Animal? Animal { get; set; }
    }
}