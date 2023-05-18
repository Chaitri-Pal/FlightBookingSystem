using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Airport_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    A_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Airport_Id);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Reward_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    loyalty_value = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Reward_Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Schedule_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Flight_Id = table.Column<int>(type: "int", nullable: false),
                    dep_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arr_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dep_loc_id = table.Column<int>(type: "int", nullable: false),
                    arr_loc_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Schedule_Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Airports_arr_loc_id",
                        column: x => x.arr_loc_id,
                        principalTable: "Airports",
                        principalColumn: "Airport_Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Airports_dep_loc_id",
                        column: x => x.dep_loc_id,
                        principalTable: "Airports",
                        principalColumn: "Airport_Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Booking_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Schedule_Id = table.Column<int>(type: "int", nullable: false),
                    Cust_ID = table.Column<int>(type: "int", nullable: false),
                    Booking_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    B_status = table.Column<bool>(type: "bit", nullable: false),
                    Reward_Id = table.Column<int>(type: "int", nullable: false),
                    Customer_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Booking_Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Customer_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Rewards_Reward_Id",
                        column: x => x.Reward_Id,
                        principalTable: "Rewards",
                        principalColumn: "Reward_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Schedules_Schedule_Id",
                        column: x => x.Schedule_Id,
                        principalTable: "Schedules",
                        principalColumn: "Schedule_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Flight_Id = table.Column<int>(type: "int", nullable: false),
                    Flight_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seat_capacity = table.Column<int>(type: "int", nullable: false),
                    VacantSeat_capacity = table.Column<int>(type: "int", nullable: false),
                    Weight_limit = table.Column<int>(type: "int", nullable: false),
                    Flying_hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Flight_Id);
                    table.ForeignKey(
                        name: "FK_Flights_Schedules_Flight_Id",
                        column: x => x.Flight_Id,
                        principalTable: "Schedules",
                        principalColumn: "Schedule_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Payment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_Id = table.Column<int>(type: "int", nullable: false),
                    Cust_ID = table.Column<int>(type: "int", nullable: false),
                    P_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P_status = table.Column<bool>(type: "bit", nullable: false),
                    Payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Reward_Id = table.Column<int>(type: "int", nullable: false),
                    Customer_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Payment_Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_Booking_Id",
                        column: x => x.Booking_Id,
                        principalTable: "Bookings",
                        principalColumn: "Booking_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_Cust_ID",
                        column: x => x.Cust_ID,
                        principalTable: "Customers",
                        principalColumn: "Customer_Id");
                    table.ForeignKey(
                        name: "FK_Payments_Rewards_Reward_Id",
                        column: x => x.Reward_Id,
                        principalTable: "Rewards",
                        principalColumn: "Reward_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Customer_Id",
                table: "Bookings",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Reward_Id",
                table: "Bookings",
                column: "Reward_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Schedule_Id",
                table: "Bookings",
                column: "Schedule_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Booking_Id",
                table: "Payments",
                column: "Booking_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Cust_ID",
                table: "Payments",
                column: "Cust_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Reward_Id",
                table: "Payments",
                column: "Reward_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_arr_loc_id",
                table: "Schedules",
                column: "arr_loc_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_dep_loc_id",
                table: "Schedules",
                column: "dep_loc_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
