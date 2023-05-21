using FlightBookingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BAL.Contacts
{
    public interface IBookingManager
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingAsync(int id);
        Task<bool> AddBooking(Booking ai);
        void UpdateBooking(Booking ai);
        void DeleteBooking(Booking ai);
    }
}