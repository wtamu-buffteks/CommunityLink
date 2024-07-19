﻿// <auto-generated />
using System;
using CommunityLink.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommunityLink.Migrations
{
    [DbContext(typeof(CommunityLinkDbContext))]
    partial class CommunityLinkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CommunityLink.Models.ActionNeeded", b =>
                {
                    b.Property<int>("ActionNeededId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActionMessage")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<int?>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("ActionNeededId");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("UserID");

                    b.ToTable("ActionNeededs");
                });

            modelBuilder.Entity("CommunityLink.Models.Animal", b =>
                {
                    b.Property<int>("AnimalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Edible")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("InventoryID")
                        .HasColumnType("int");

                    b.Property<bool>("Perishable")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("AnimalID");

                    b.HasIndex("InventoryID")
                        .IsUnique();

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("CommunityLink.Models.CommunityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserLocation")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("UserStatus")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CommunityLink.Models.DonationStat", b =>
                {
                    b.Property<int>("DonationStatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDonated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateGiven")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RequestStatID")
                        .HasColumnType("int");

                    b.Property<int>("StatID")
                        .HasColumnType("int");

                    b.HasKey("DonationStatID");

                    b.HasIndex("RequestStatID");

                    b.HasIndex("StatID");

                    b.ToTable("DonationStats");
                });

            modelBuilder.Entity("CommunityLink.Models.Edible", b =>
                {
                    b.Property<int>("EdibleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("InventoryID")
                        .HasColumnType("int");

                    b.Property<bool>("Perishable")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("EdibleID");

                    b.HasIndex("InventoryID")
                        .IsUnique();

                    b.ToTable("Edibles");
                });

            modelBuilder.Entity("CommunityLink.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HoursWorked")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("EmployeeID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CommunityLink.Models.Inventory", b =>
                {
                    b.Property<int>("InventoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DateReceived")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ItemDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("StorageType")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<float>("UnitCost")
                        .HasColumnType("float");

                    b.Property<string>("UnitOfMeasurement")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("InventoryID");

                    b.HasIndex("EventID");

                    b.HasIndex("LocationID");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("CommunityLink.Models.InventoryLocation", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<bool>("Cold")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Frozen")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LocationAddress")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("LocationID");

                    b.ToTable("InventoryLocations");
                });

            modelBuilder.Entity("CommunityLink.Models.LocationPhone", b =>
                {
                    b.Property<int>("PhoneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ContactName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.HasKey("PhoneID");

                    b.HasIndex("LocationID");

                    b.ToTable("LocationPhones");
                });

            modelBuilder.Entity("CommunityLink.Models.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Read")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ReceiverID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SenderID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SenderMessage")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("MessageID");

                    b.HasIndex("ReceiverID");

                    b.HasIndex("SenderID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("CommunityLink.Models.Nonedible", b =>
                {
                    b.Property<int>("NonedibleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("InventoryID")
                        .HasColumnType("int");

                    b.HasKey("NonedibleID");

                    b.HasIndex("InventoryID")
                        .IsUnique();

                    b.ToTable("Nonedibles");
                });

            modelBuilder.Entity("CommunityLink.Models.PlannedEvent", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EventDescription")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<string>("EventLocation")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("RSVP")
                        .HasColumnType("int");

                    b.HasKey("EventID");

                    b.ToTable("PlannedEvents");
                });

            modelBuilder.Entity("CommunityLink.Models.Request", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("AmountRecieved")
                        .HasColumnType("float");

                    b.Property<float>("AmountRequested")
                        .HasColumnType("float");

                    b.Property<string>("Category")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RequestDeadline")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RequestDescription")
                        .HasMaxLength(3000)
                        .HasColumnType("varchar(3000)");

                    b.Property<string>("RequestStatus")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("RequestTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("RequestorID")
                        .HasColumnType("int");

                    b.HasKey("RequestID");

                    b.HasIndex("RequestorID");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("CommunityLink.Models.RequestStat", b =>
                {
                    b.Property<int>("RequestStatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("AmountDonated")
                        .HasColumnType("float");

                    b.Property<DateTime>("DonationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OldRequestID")
                        .HasColumnType("int");

                    b.Property<string>("RequestTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("RequestorUsername")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("StatID")
                        .HasColumnType("int");

                    b.HasKey("RequestStatID");

                    b.HasIndex("StatID");

                    b.ToTable("RequestStats");
                });

            modelBuilder.Entity("CommunityLink.Models.Requestor", b =>
                {
                    b.Property<int>("RequestorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("RequestorID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Requestors");
                });

            modelBuilder.Entity("CommunityLink.Models.Stat", b =>
                {
                    b.Property<int>("StatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("MoneyDonated")
                        .HasColumnType("float");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("StatID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("CommunityLink.Models.UsersAttendingEvent", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "EventID");

                    b.HasIndex("EventID");

                    b.ToTable("UsersAttendingEvent");
                });

            modelBuilder.Entity("CommunityLink.Models.Volunteer", b =>
                {
                    b.Property<int>("VolunteerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HoursWorked")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("VolunteerID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Volunteers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CommunityLink.Models.ActionNeeded", b =>
                {
                    b.HasOne("CommunityLink.Models.Employee", "Employee")
                        .WithMany("ActionNeededs")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CommunityLink.Models.CommunityUser", "User")
                        .WithMany("ActionNeededs")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommunityLink.Models.Animal", b =>
                {
                    b.HasOne("CommunityLink.Models.Inventory", "Inventory")
                        .WithOne("Animal")
                        .HasForeignKey("CommunityLink.Models.Animal", "InventoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("CommunityLink.Models.DonationStat", b =>
                {
                    b.HasOne("CommunityLink.Models.RequestStat", "RequestStat")
                        .WithMany("DonationStats")
                        .HasForeignKey("RequestStatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommunityLink.Models.Stat", "Stat")
                        .WithMany("DonationStats")
                        .HasForeignKey("StatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RequestStat");

                    b.Navigation("Stat");
                });

            modelBuilder.Entity("CommunityLink.Models.Edible", b =>
                {
                    b.HasOne("CommunityLink.Models.Inventory", "Inventory")
                        .WithOne("Edible")
                        .HasForeignKey("CommunityLink.Models.Edible", "InventoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("CommunityLink.Models.Employee", b =>
                {
                    b.HasOne("CommunityLink.Models.CommunityUser", "User")
                        .WithOne("Employee")
                        .HasForeignKey("CommunityLink.Models.Employee", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommunityLink.Models.Inventory", b =>
                {
                    b.HasOne("CommunityLink.Models.PlannedEvent", "PlannedEvent")
                        .WithMany("Inventory")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CommunityLink.Models.InventoryLocation", "InventoryLocation")
                        .WithMany("Inventory")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("InventoryLocation");

                    b.Navigation("PlannedEvent");
                });

            modelBuilder.Entity("CommunityLink.Models.LocationPhone", b =>
                {
                    b.HasOne("CommunityLink.Models.InventoryLocation", "InventoryLocation")
                        .WithMany("LocationPhone")
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InventoryLocation");
                });

            modelBuilder.Entity("CommunityLink.Models.Message", b =>
                {
                    b.HasOne("CommunityLink.Models.CommunityUser", "Receiver")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("ReceiverID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommunityLink.Models.CommunityUser", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("CommunityLink.Models.Nonedible", b =>
                {
                    b.HasOne("CommunityLink.Models.Inventory", "Inventory")
                        .WithOne("Nonedible")
                        .HasForeignKey("CommunityLink.Models.Nonedible", "InventoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("CommunityLink.Models.Request", b =>
                {
                    b.HasOne("CommunityLink.Models.Requestor", "Requestor")
                        .WithMany("Requests")
                        .HasForeignKey("RequestorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Requestor");
                });

            modelBuilder.Entity("CommunityLink.Models.RequestStat", b =>
                {
                    b.HasOne("CommunityLink.Models.Stat", "Stat")
                        .WithMany("RequestStats")
                        .HasForeignKey("StatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stat");
                });

            modelBuilder.Entity("CommunityLink.Models.Requestor", b =>
                {
                    b.HasOne("CommunityLink.Models.CommunityUser", "User")
                        .WithOne("Requestor")
                        .HasForeignKey("CommunityLink.Models.Requestor", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommunityLink.Models.Stat", b =>
                {
                    b.HasOne("CommunityLink.Models.CommunityUser", "User")
                        .WithOne("Stat")
                        .HasForeignKey("CommunityLink.Models.Stat", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommunityLink.Models.UsersAttendingEvent", b =>
                {
                    b.HasOne("CommunityLink.Models.PlannedEvent", "PlannedEvent")
                        .WithMany("UsersAttendingEvent")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommunityLink.Models.CommunityUser", "User")
                        .WithMany("UsersAttendingEvent")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlannedEvent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommunityLink.Models.Volunteer", b =>
                {
                    b.HasOne("CommunityLink.Models.CommunityUser", "User")
                        .WithOne("Volunteer")
                        .HasForeignKey("CommunityLink.Models.Volunteer", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CommunityLink.Models.CommunityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CommunityLink.Models.CommunityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommunityLink.Models.CommunityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CommunityLink.Models.CommunityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommunityLink.Models.CommunityUser", b =>
                {
                    b.Navigation("ActionNeededs");

                    b.Navigation("Employee");

                    b.Navigation("MessagesReceived");

                    b.Navigation("MessagesSent");

                    b.Navigation("Requestor");

                    b.Navigation("Stat");

                    b.Navigation("UsersAttendingEvent");

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("CommunityLink.Models.Employee", b =>
                {
                    b.Navigation("ActionNeededs");
                });

            modelBuilder.Entity("CommunityLink.Models.Inventory", b =>
                {
                    b.Navigation("Animal");

                    b.Navigation("Edible");

                    b.Navigation("Nonedible");
                });

            modelBuilder.Entity("CommunityLink.Models.InventoryLocation", b =>
                {
                    b.Navigation("Inventory");

                    b.Navigation("LocationPhone");
                });

            modelBuilder.Entity("CommunityLink.Models.PlannedEvent", b =>
                {
                    b.Navigation("Inventory");

                    b.Navigation("UsersAttendingEvent");
                });

            modelBuilder.Entity("CommunityLink.Models.RequestStat", b =>
                {
                    b.Navigation("DonationStats");
                });

            modelBuilder.Entity("CommunityLink.Models.Requestor", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("CommunityLink.Models.Stat", b =>
                {
                    b.Navigation("DonationStats");

                    b.Navigation("RequestStats");
                });
#pragma warning restore 612, 618
        }
    }
}
