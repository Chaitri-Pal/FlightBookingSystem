using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBookingSystem.DAL.Model;

namespace FlightBookingSystem.DAL.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //process of linking 2 columns of 1 table to 1 primary key and due to multiple cascade paths ondelete option is give
            //modelBuilder.Entity<Schedule>()
            //  .HasKey(k => k.Id);

            modelBuilder.Entity<Schedule>()
                .HasOne(a => a.arrivalAirport)
                .WithMany(s => s.arrivalSchedule)
                .HasForeignKey(a => a.Arr_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Schedule>()
                .HasOne(d => d.departureAirport)
                .WithMany(s => s.departureSchedule)
                .HasForeignKey(d => d.Dep_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Schedule>()
                .HasOne(f => f.flights)
                .WithMany(s => s.schedules)
                .HasForeignKey(f => f.Flight_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(c => c.users)
                .WithMany(b => b.bookings)
                .HasForeignKey(c => c.User_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
               .HasOne(r => r.rewards)
               .WithMany(b => b.bookings)
               .HasForeignKey(r => r.Reward_Id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
               .HasOne(s => s.schedules)
               .WithMany(b => b.bookings)
               .HasForeignKey(s => s.Schedule_Id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
               .HasOne(c => c.users)
               .WithMany(p => p.payments)
               .HasForeignKey(c => c.User_Id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
               .HasOne(b => b.bookings)
               .WithMany(p => p.payments)
               .HasForeignKey(b => b.Booking_Id)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(r => r.rewards)
                .WithMany(p => p.payments)
                .HasForeignKey(r => r.Reward_Id)
                .OnDelete(DeleteBehavior.NoAction);


        }

    }
}

