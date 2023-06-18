using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Userupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_Customer_Id",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_Customer_Id",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Customer_Id",
                table: "Payments",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_Customer_Id",
                table: "Payments",
                newName: "IX_Payments_User_Id");

            migrationBuilder.RenameColumn(
                name: "C_Name",
                table: "Customers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Customer_Id",
                table: "Bookings",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_Customer_Id",
                table: "Bookings",
                newName: "IX_Bookings_User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_User_Id",
                table: "Bookings",
                column: "User_Id",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_User_Id",
                table: "Payments",
                column: "User_Id",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_User_Id",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_User_Id",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Payments",
                newName: "Customer_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_User_Id",
                table: "Payments",
                newName: "IX_Payments_Customer_Id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "C_Name");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Bookings",
                newName: "Customer_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_User_Id",
                table: "Bookings",
                newName: "IX_Bookings_Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_Customer_Id",
                table: "Bookings",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_Customer_Id",
                table: "Payments",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
