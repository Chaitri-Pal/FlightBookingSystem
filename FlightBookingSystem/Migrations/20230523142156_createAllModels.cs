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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    A_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Flight_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seat_Capacity = table.Column<int>(type: "int", nullable: false),
                    Vacant_Seat = table.Column<int>(type: "int", nullable: false),
                    Weight_Limit = table.Column<int>(type: "int", nullable: false),
                    Flying_Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loyalty_Value = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dep_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arr_Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dep_id = table.Column<int>(type: "int", nullable: false),
                    Arr_id = table.Column<int>(type: "int", nullable: false),
                    Flight_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Airports_Arr_id",
                        column: x => x.Arr_id,
                        principalTable: "Airports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Airports_Dep_id",
                        column: x => x.Dep_id,
                        principalTable: "Airports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Flights_Flight_Id",
                        column: x => x.Flight_Id,
                        principalTable: "Flights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    B_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Schedule_Id = table.Column<int>(type: "int", nullable: false),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    Reward_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Rewards_Reward_Id",
                        column: x => x.Reward_Id,
                        principalTable: "Rewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Schedules_Schedule_Id",
                        column: x => x.Schedule_Id,
                        principalTable: "Schedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    Booking_Id = table.Column<int>(type: "int", nullable: false),
                    Reward_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_Booking_Id",
                        column: x => x.Booking_Id,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Rewards_Reward_Id",
                        column: x => x.Reward_Id,
                        principalTable: "Rewards",
                        principalColumn: "Id");
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
                name: "IX_Payments_Customer_Id",
                table: "Payments",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Reward_Id",
                table: "Payments",
                column: "Reward_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Arr_id",
                table: "Schedules",
                column: "Arr_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Dep_id",
                table: "Schedules",
                column: "Dep_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Flight_Id",
                table: "Schedules",
                column: "Flight_Id");
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
