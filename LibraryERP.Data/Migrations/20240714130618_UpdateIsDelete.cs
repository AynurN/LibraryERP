using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIsDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Loans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "LoanItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "BookAuthors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "LoanItems");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "BookAuthors");
        }
    }
}
