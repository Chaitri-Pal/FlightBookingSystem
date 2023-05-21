using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class createAllModels : Migration
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
                name: "Customers",
                columns: table => new
                {
                    Customer_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    C_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aadhar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Customer_Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Flight_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Flight_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seat_Capacity = table.Column<int>(type: "int", nullable: false),
                    Vacant_Seats = table.Column<int>(type: "int", nullable: false),
                    Weight_limit = table.Column<int>(type: "int", nullable: false),
                    Flying_hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Flight_ID);
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
                    Dep_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arr_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dep_id = table.Column<int>(type: "int", nullable: false),
                    Arr_id = table.Column<int>(type: "int", nullable: false),
                    Flight_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Schedule_Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Airports_Arr_id",
                        column: x => x.Arr_id,
                        principalTable: "Airports",
                        principalColumn: "Airport_Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Airports_Dep_id",
                        column: x => x.Dep_id,
                        principalTable: "Airports",
                        principalColumn: "Airport_Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Flights_Flight_ID",
                        column: x => x.Flight_ID,
                        principalTable: "Flights",
                        principalColumn: "Flight_ID");
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
                    Reward_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Booking_Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_Cust_ID",
                        column: x => x.Cust_ID,
                        principalTable: "Customers",
                        principalColumn: "Customer_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Rewards_Reward_Id",
                        column: x => x.Reward_Id,
                        principalTable: "Rewards",
                        principalColumn: "Reward_Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Schedules_Schedule_Id",
                        column: x => x.Schedule_Id,
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
                    P_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P_status = table.Column<bool>(type: "bit", nullable: false),
                    Payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Customer_Id = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Payments_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Customer_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Cust_ID",
                table: "Bookings",
                column: "Cust_ID");

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
                name: "IX_Payments_Customer_Id",
                table: "Payments",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Arr_id",
                table: "Schedules",
                column: "Arr_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Dep_id",
                table: "Schedules",
                column: "Dep_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Flight_ID",
                table: "Schedules",
                column: "Flight_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
