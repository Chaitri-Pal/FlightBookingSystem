using FlightBookingSystem.DAL.Model;
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
        Task<IEnumerable<Booking>> GetByUserId(int Uid);
        Task<Booking> GetBookingAsync(int id);
        Task<int> AddBooking(Booking bk);
        void UpdateBooking(Booking bk);
        void DeleteBooking(Booking bk);
    }
}