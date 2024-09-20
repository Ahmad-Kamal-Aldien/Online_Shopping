using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addtrackingnumberfororder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackingNumber",
                table: "orderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                table: "orderHeaders");
        }
    }
}
