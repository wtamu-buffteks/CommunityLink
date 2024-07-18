// using CommunityLink.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommunityLink.Models
{
    public class CommunityLinkDbContext : IdentityDbContext<CommunityUser, IdentityRole, string>
    {
        public CommunityLinkDbContext(DbContextOptions<CommunityLinkDbContext> options)
            : base(options) { }

        // DbSet properties
        public DbSet<CommunityUser> Users { get; set; } = default!;

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
        public DbSet<LocationPhone> LocationPhones { get; set; } = default!;
        public DbSet<PlannedEvent> PlannedEvents { get; set; } = default!;
        public DbSet<UsersAttendingEvent> UsersAttendingEvent { get; set; } = default!;

        // Very important to add `base.OnModelCreating(modelBuilder);` to enable identity scaffolding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Custom configurations for entities
            // CommunityUser to Employee relationship
            modelBuilder
                .Entity<CommunityUser>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<Employee>(e => e.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // // CommunityUser to Volunteer relationship
            modelBuilder
                .Entity<CommunityUser>()
                .HasOne(u => u.Volunteer)
                .WithOne(v => v.User)
                .HasForeignKey<Volunteer>(v => v.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // // CommunityUser to Requestor relationship
            modelBuilder
                .Entity<CommunityUser>()
                .HasOne(u => u.Requestor)
                .WithOne(r => r.User)
                .HasForeignKey<Requestor>(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // CommunityUser to Message (Sent and Received) relationships
            modelBuilder
                .Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder
                .Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.MessagesReceived)
                .HasForeignKey(m => m.ReceiverID)
                .OnDelete(DeleteBehavior.Cascade);

            // // CommunityUser to ActionNeeded relationship
            modelBuilder
                .Entity<ActionNeeded>()
                .HasOne(an => an.User)
                .WithMany(u => u.ActionNeededs)
                .HasForeignKey(an => an.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // // Employee to ActionNeeded relationship
            modelBuilder
                .Entity<ActionNeeded>()
                .HasOne(an => an.Employee)
                .WithMany(e => e.ActionNeededs)
                .HasForeignKey(an => an.EmployeeID)
                .OnDelete(DeleteBehavior.SetNull);

            // CommunityUser to Stat relationship
            modelBuilder
                .Entity<CommunityUser>()
                .HasOne(u => u.Stat)
                .WithOne(s => s.User)
                .HasForeignKey<Stat>(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // // Stat to DonationStat relationship
            modelBuilder
                .Entity<Stat>()
                .HasMany(s => s.DonationStats)
                .WithOne(ds => ds.Stat)
                .HasForeignKey(ds => ds.StatID)
                .OnDelete(DeleteBehavior.Cascade);

            // Stat to RequestStat relationship
            modelBuilder
                .Entity<Stat>()
                .HasMany(s => s.RequestStats)
                .WithOne(rs => rs.Stat)
                .HasForeignKey(rs => rs.StatID)
                .OnDelete(DeleteBehavior.Cascade);

            // RequestStat to DonationStat relationship
            modelBuilder
                .Entity<RequestStat>()
                .HasMany(rs => rs.DonationStats)
                .WithOne(ds => ds.RequestStat)
                .HasForeignKey(ds => ds.RequestStatID)
                .OnDelete(DeleteBehavior.Cascade);

            // CommunityUser to UsersAttending relationship
            modelBuilder.Entity<UsersAttendingEvent>().HasKey(ua => new { ua.UserID, ua.EventID });

            modelBuilder
                .Entity<UsersAttendingEvent>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UsersAttendingEvent)
                .HasForeignKey(ua => ua.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<UsersAttendingEvent>()
                .HasOne(ua => ua.PlannedEvent)
                .WithMany(pe => pe.UsersAttendingEvent)
                .HasForeignKey(ua => ua.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            // Requestor to Request relationship
            modelBuilder
                .Entity<Requestor>()
                .HasMany(r => r.Requests)
                .WithOne(req => req.Requestor)
                .HasForeignKey(req => req.RequestorID)
                .OnDelete(DeleteBehavior.Cascade);

            // Inventory to Edible relationship
            modelBuilder
                .Entity<Inventory>()
                .HasOne(i => i.Edible)
                .WithOne(e => e.Inventory)
                .HasForeignKey<Edible>(e => e.InventoryID)
                .OnDelete(DeleteBehavior.Cascade);

            // Inventory to Nonedible relationship
            modelBuilder
                .Entity<Inventory>()
                .HasOne(i => i.Nonedible)
                .WithOne(n => n.Inventory)
                .HasForeignKey<Nonedible>(n => n.InventoryID)
                .OnDelete(DeleteBehavior.Cascade);

            // Inventory to Animal relationship
            modelBuilder
                .Entity<Inventory>()
                .HasOne(i => i.Animal)
                .WithOne(a => a.Inventory)
                .HasForeignKey<Animal>(a => a.InventoryID)
                .OnDelete(DeleteBehavior.Cascade);

            // InventoryLocation to Inventory relationship
            modelBuilder
                .Entity<InventoryLocation>()
                .HasMany(il => il.Inventory)
                .WithOne(i => i.InventoryLocation)
                .HasForeignKey(i => i.LocationID)
                .OnDelete(DeleteBehavior.Restrict);

            // PlannedEvent to Inventory relationship
            modelBuilder
                .Entity<PlannedEvent>()
                .HasMany(pe => pe.Inventory)
                .WithOne(i => i.PlannedEvent)
                .HasForeignKey(i => i.EventID)
                .OnDelete(DeleteBehavior.Restrict);

            // InventoryLocation to InventoryPhone relationship
            modelBuilder
                .Entity<InventoryLocation>()
                .HasMany(il => il.LocationPhone)
                .WithOne(ip => ip.InventoryLocation)
                .HasForeignKey(ip => ip.LocationID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
