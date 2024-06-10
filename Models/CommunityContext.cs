using CharityLink.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityLink.Models {
    public class CommunityLinkDbContext : DbContext {
        public CommunityLinkDbContext (DbContextOptions<CommunityLinkDbContext> options)
            : base(options) {}
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Volunteer> Volunteers { get; set; } = default!;
        public DbSet<Requestor> Requestors { get; set; } = default!;
        public DbSet<Request> Requests { get; set; } = default!;
        public DbSet<Message> Messages { get; set; } = default!;
        public DbSet<ActionNeeded> ActionNeededs { get; set; } = default!;
        public DbSet<Stat> Stats { get; set; } = default!;
        public DbSet<DonationStat> DonationStats { get; set; } = default!;
        public DbSet<RequestStat> RequestStats { get; set; } = default!;
        public DbSet<Inventory> Inventory { get; set; } = default!;
        public DbSet<Nonedible> Nonedibles { get; set; } = default!;
        public DbSet<Edible> Edibles { get; set; } = default!;
        public DbSet<Animal> Animals { get; set; } = default!;
        public DbSet<InventoryLocation> InventoryLocations { get; set; } = default!;
        public DbSet<InventoryPhone> InventoryPhones { get; set; } = default!;
        public DbSet<PlannedEvent> PlannedEvents { get; set; } = default!;
        public DbSet<UsersAttending> UsersAttending { get; set; } = default!;
    }
}