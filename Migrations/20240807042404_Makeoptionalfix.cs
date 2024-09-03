using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityLink.Migrations
{
    /// <inheritdoc />
    public partial class Makeoptionalfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_InventoryLocations_LocationID",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_PlannedEvents_EventID",
                table: "Inventory");

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Inventory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventID",
                table: "Inventory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_InventoryLocations_LocationID",
                table: "Inventory",
                column: "LocationID",
                principalTable: "InventoryLocations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_PlannedEvents_EventID",
                table: "Inventory",
                column: "EventID",
                principalTable: "PlannedEvents",
                principalColumn: "EventID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_InventoryLocations_LocationID",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_PlannedEvents_EventID",
                table: "Inventory");

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventID",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_InventoryLocations_LocationID",
                table: "Inventory",
                column: "LocationID",
                principalTable: "InventoryLocations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_PlannedEvents_EventID",
                table: "Inventory",
                column: "EventID",
                principalTable: "PlannedEvents",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
