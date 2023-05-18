﻿// <auto-generated />
using System;
using FlightBookingSystem.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightBookingSystem.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230517155857_AllTables")]
    partial class AllTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Airport", b =>
                {
                    b.Property<int>("Airport_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Airport_Id"));

                    b.Property<string>("A_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Airport_Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Booking", b =>
                {
                    b.Property<int>("Booking_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Booking_Id"));

                    b.Property<bool>("B_status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Booking_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Cust_ID")
                        .HasColumnType("int");

                    b.Property<int>("Customer_Id")
                        .HasColumnType("int");

                    b.Property<int>("Reward_Id")
                        .HasColumnType("int");

                    b.Property<int>("Schedule_Id")
                        .HasColumnType("int");

                    b.HasKey("Booking_Id");

                    b.HasIndex("Customer_Id");

                    b.HasIndex("Reward_Id");

                    b.HasIndex("Schedule_Id");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Customer", b =>
                {
                    b.Property<int>("Customer_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Customer_Id"));

                    b.Property<string>("Aadhar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.HasKey("Customer_Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Flight", b =>
                {
                    b.Property<int>("Flight_Id")
                        .HasColumnType("int");

                    b.Property<string>("Flight_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Flying_hours")
                        .HasColumnType("int");

                    b.Property<int>("Seat_capacity")
                        .HasColumnType("int");

                    b.Property<int>("VacantSeat_capacity")
                        .HasColumnType("int");

                    b.Property<int>("Weight_limit")
                        .HasColumnType("int");

                    b.HasKey("Flight_Id");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Payment", b =>
                {
                    b.Property<int>("Payment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Payment_Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("Booking_Id")
                        .HasColumnType("int");

                    b.Property<int>("Cust_ID")
                        .HasColumnType("int");

                    b.Property<int>("Customer_Id")
                        .HasColumnType("int");

                    b.Property<bool>("P_status")
                        .HasColumnType("bit");

                    b.Property<string>("P_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Payment_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Reward_Id")
                        .HasColumnType("int");

                    b.HasKey("Payment_Id");

                    b.HasIndex("Booking_Id");

                    b.HasIndex("Cust_ID");

                    b.HasIndex("Reward_Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Reward", b =>
                {
                    b.Property<int>("Reward_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Reward_Id"));

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<int>("loyalty_value")
                        .HasColumnType("int");

                    b.HasKey("Reward_Id");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Schedule", b =>
                {
                    b.Property<int>("Schedule_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Schedule_Id"));

                    b.Property<int>("Flight_Id")
                        .HasColumnType("int");

                    b.Property<int>("arr_loc_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("arr_time")
                        .HasColumnType("datetime2");

                    b.Property<int>("dep_loc_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("dep_time")
                        .HasColumnType("datetime2");

                    b.HasKey("Schedule_Id");

                    b.HasIndex("arr_loc_id");

                    b.HasIndex("dep_loc_id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Booking", b =>
                {
                    b.HasOne("FlightBookingSystem.DAL.Models.Customer", "customer")
                        .WithMany("booking")
                        .HasForeignKey("Customer_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightBookingSystem.DAL.Models.Reward", "reward")
                        .WithMany("booking")
                        .HasForeignKey("Reward_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightBookingSystem.DAL.Models.Schedule", "schedule")
                        .WithMany("booking")
                        .HasForeignKey("Schedule_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("reward");

                    b.Navigation("schedule");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Flight", b =>
                {
                    b.HasOne("FlightBookingSystem.DAL.Models.Schedule", "schedule")
                        .WithMany("flight")
                        .HasForeignKey("Flight_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("schedule");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Payment", b =>
                {
                    b.HasOne("FlightBookingSystem.DAL.Models.Booking", "booking")
                        .WithMany("payment")
                        .HasForeignKey("Booking_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightBookingSystem.DAL.Models.Customer", "customer")
                        .WithMany("payment")
                        .HasForeignKey("Cust_ID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FlightBookingSystem.DAL.Models.Reward", "reward")
                        .WithMany("payment")
                        .HasForeignKey("Reward_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("booking");

                    b.Navigation("customer");

                    b.Navigation("reward");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Schedule", b =>
                {
                    b.HasOne("FlightBookingSystem.DAL.Models.Airport", "arrivalairport")
                        .WithMany("arrivalschedule")
                        .HasForeignKey("arr_loc_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FlightBookingSystem.DAL.Models.Airport", "departureairport")
                        .WithMany("departureschedule")
                        .HasForeignKey("dep_loc_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("arrivalairport");

                    b.Navigation("departureairport");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Airport", b =>
                {
                    b.Navigation("arrivalschedule");

                    b.Navigation("departureschedule");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Booking", b =>
                {
                    b.Navigation("payment");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Customer", b =>
                {
                    b.Navigation("booking");

                    b.Navigation("payment");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Reward", b =>
                {
                    b.Navigation("booking");

                    b.Navigation("payment");
                });

            modelBuilder.Entity("FlightBookingSystem.DAL.Models.Schedule", b =>
                {
                    b.Navigation("booking");

                    b.Navigation("flight");
                });
#pragma warning restore 612, 618
        }
    }
}
