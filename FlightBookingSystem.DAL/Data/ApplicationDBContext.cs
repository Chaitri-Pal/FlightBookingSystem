using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBookingSystem.DAL.Models;


namespace FlightBookingSystem.DAL.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
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
             modelBuilder.Entity<Schedule>()
                 .HasKey(k => k.Schedule_Id);
             modelBuilder.Entity<Schedule>()
                 .HasOne(a => a.arrivalAirport)
                 .WithMany(s => s.arrivalschedule)
                 .HasForeignKey(a => a.Arr_id)
                 .OnDelete(DeleteBehavior.NoAction);

             modelBuilder.Entity<Schedule>()
                 .HasOne(d => d.departureAirport)
                 .WithMany(s => s.departureschedule)
                 .HasForeignKey(d => d.Dep_id)
                 .OnDelete(DeleteBehavior.NoAction);

            
                




            /*modelBuilder.Entity<Payment>()
                .HasOne(c => c.customer)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

           /* modelBuilder.Entity<Payment>()
                .HasOne(c => c.reward)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);*/

        }
    }
}

