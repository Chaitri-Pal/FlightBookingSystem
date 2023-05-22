using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BAL.Services
{
    public class BookingManager : IBookingManager
    {
        private readonly IUnitOfWork _da;
        public BookingManager(IUnitOfWork da)
        {
            _da = da;
        }
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _da.Booking.GetAllAsync();
        }

        public async Task<Booking> GetBookingAsync(int id)
        {
            return await _da.Booking.GetFirstorDefaultAsync(x => x.Booking_Id == id);
        }

        public async Task<bool> AddBooking(Booking bk)
        {
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
            if (bk != null)
            {
                IEnumerable<Booking> book = await _da.Booking.GetAllAsync();
                if (book.Any(x => x.Schedule_Id.Equals(bk.Schedule_Id) && x.Cust_ID.Equals(bk.Cust_ID)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var obs = new Booking();
                    obs.Schedule_Id = bk.Schedule_Id;
                    obs.Cust_ID= bk.Cust_ID;
                    obs.Booking_date = bk.Booking_date;
                    obs.B_status = bk.B_status;

                    // obs.Reward_Id = bk.Reward_Id;
                    _da.Booking.AddAsync(obs);
                    _da.Save();
                    return await Task.FromResult(true);
                }

            }
            else
            {
                return false;
            }
        }

        public void UpdateBooking(Booking bk)
        {
            _da.Booking.UpdateExisting(bk);
            _da.Save();
        }

        public void DeleteBooking(Booking bk)
        {
            _da.Booking.Remove(bk);
            _da.Save();
        }

        
    }
}