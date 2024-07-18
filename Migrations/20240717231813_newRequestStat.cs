using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityLink.Migrations
{
    /// <inheritdoc />
    public partial class newRequestStat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OldRequestID",
                table: "RequestStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldRequestID",
                table: "RequestStats");
        }
    }
}
