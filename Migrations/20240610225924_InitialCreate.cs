using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CommunityLink.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventoryLocations",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LocationName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LocationAddress = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Cold = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Frozen = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLocations", x => x.LocationID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlannedEvents",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RSVP = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    EventDescription = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EventLocation = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedEvents", x => x.EventID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    UserStatus = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    UserLocation = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventoryPhones",
                columns: table => new
                {
                    PhoneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ContactName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryPhones", x => x.PhoneID);
                    table.ForeignKey(
                        name: "FK_InventoryPhones_InventoryLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "InventoryLocations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ItemDescription = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    UnitCost = table.Column<float>(type: "float", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Barcode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    StorageType = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryID);
                    table.ForeignKey(
                        name: "FK_Inventory_InventoryLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "InventoryLocations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_PlannedEvents_EventID",
                        column: x => x.EventID,
                        principalTable: "PlannedEvents",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    HoursWorked = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SenderID = table.Column<int>(type: "int", nullable: true),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    SenderMessage = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false),
                    Read = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Requestors",
                columns: table => new
                {
                    RequestorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requestors", x => x.RequestorID);
                    table.ForeignKey(
                        name: "FK_Requestors_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    StatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MoneyDonated = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.StatID);
                    table.ForeignKey(
                        name: "FK_Stats_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsersAttending",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAttending", x => new { x.UserID, x.EventID });
                    table.ForeignKey(
                        name: "FK_UsersAttending_PlannedEvents_EventID",
                        column: x => x.EventID,
                        principalTable: "PlannedEvents",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersAttending_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    VolunteerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    HoursWorked = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.VolunteerID);
                    table.ForeignKey(
                        name: "FK_Volunteers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    InventoryID = table.Column<int>(type: "int", nullable: false),
                    Edible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Perishable = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalID);
                    table.ForeignKey(
                        name: "FK_Animals_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "InventoryID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Edibles",
                columns: table => new
                {
                    EdibleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    InventoryID = table.Column<int>(type: "int", nullable: false),
                    Perishable = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edibles", x => x.EdibleID);
                    table.ForeignKey(
                        name: "FK_Edibles_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "InventoryID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Nonedibles",
                columns: table => new
                {
                    NonedibleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    InventoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nonedibles", x => x.NonedibleID);
                    table.ForeignKey(
                        name: "FK_Nonedibles_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "InventoryID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActionNeededs",
                columns: table => new
                {
                    ActionNeededId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ActionMessage = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionNeededs", x => x.ActionNeededId);
                    table.ForeignKey(
                        name: "FK_ActionNeededs_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ActionNeededs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RequestorID = table.Column<int>(type: "int", nullable: false),
                    AmountRequested = table.Column<float>(type: "float", nullable: false),
                    AmountRecieved = table.Column<float>(type: "float", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RequestTitle = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    RequestDeadline = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RequestDescription = table.Column<string>(type: "varchar(3000)", maxLength: 3000, nullable: true),
                    RequestStatus = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    Category = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_Requests_Requestors_RequestorID",
                        column: x => x.RequestorID,
                        principalTable: "Requestors",
                        principalColumn: "RequestorID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RequestStats",
                columns: table => new
                {
                    RequestStatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StatID = table.Column<int>(type: "int", nullable: false),
                    RequestorUsername = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    AmountDonated = table.Column<float>(type: "float", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RequestTitle = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStats", x => x.RequestStatID);
                    table.ForeignKey(
                        name: "FK_RequestStats_Stats_StatID",
                        column: x => x.StatID,
                        principalTable: "Stats",
                        principalColumn: "StatID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DonationStats",
                columns: table => new
                {
                    DonationStatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StatID = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DateDonated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateGiven = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RequestStatID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationStats", x => x.DonationStatID);
                    table.ForeignKey(
                        name: "FK_DonationStats_RequestStats_RequestStatID",
                        column: x => x.RequestStatID,
                        principalTable: "RequestStats",
                        principalColumn: "RequestStatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationStats_Stats_StatID",
                        column: x => x.StatID,
                        principalTable: "Stats",
                        principalColumn: "StatID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ActionNeededs_EmployeeID",
                table: "ActionNeededs",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionNeededs_UserID",
                table: "ActionNeededs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_InventoryID",
                table: "Animals",
                column: "InventoryID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonationStats_RequestStatID",
                table: "DonationStats",
                column: "RequestStatID");

            migrationBuilder.CreateIndex(
                name: "IX_DonationStats_StatID",
                table: "DonationStats",
                column: "StatID");

            migrationBuilder.CreateIndex(
                name: "IX_Edibles_InventoryID",
                table: "Edibles",
                column: "InventoryID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserID",
                table: "Employees",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_EventID",
                table: "Inventory",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_LocationID",
                table: "Inventory",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPhones_LocationID",
                table: "InventoryPhones",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverID",
                table: "Messages",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderID",
                table: "Messages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Nonedibles_InventoryID",
                table: "Nonedibles",
                column: "InventoryID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requestors_UserID",
                table: "Requestors",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestorID",
                table: "Requests",
                column: "RequestorID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStats_StatID",
                table: "RequestStats",
                column: "StatID");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_UserID",
                table: "Stats",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersAttending_EventID",
                table: "UsersAttending",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_UserID",
                table: "Volunteers",
                column: "UserID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionNeededs");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "DonationStats");

            migrationBuilder.DropTable(
                name: "Edibles");

            migrationBuilder.DropTable(
                name: "InventoryPhones");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Nonedibles");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "UsersAttending");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "RequestStats");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Requestors");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "InventoryLocations");

            migrationBuilder.DropTable(
                name: "PlannedEvents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
