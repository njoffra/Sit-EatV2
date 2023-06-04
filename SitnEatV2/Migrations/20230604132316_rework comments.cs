using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SitnEatV2.Migrations
{
    /// <inheritdoc />
    public partial class reworkcomments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Comment");
        }
    }
}
