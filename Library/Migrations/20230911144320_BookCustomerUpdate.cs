using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class BookCustomerUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_IssuedBookId",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IssuedBookId",
                table: "Customer",
                column: "IssuedBookId",
                unique: true,
                filter: "[IssuedBookId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_IssuedBookId",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IssuedBookId",
                table: "Customer",
                column: "IssuedBookId");
        }
    }
}
