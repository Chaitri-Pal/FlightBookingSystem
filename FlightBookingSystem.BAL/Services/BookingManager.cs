using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;
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
            return await _da.Booking.GetFirstorDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddBooking(Booking bk)
        {
            IEnumerable<Schedule> sh = await _da.Schedule.GetAllAsync();
            IEnumerable<Customer> cs = await _da.Customer.GetAllAsync();
            IEnumerable<Reward> re = await _da.Reward.GetAllAsync();
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
            if (bk != null)
            {
                IEnumerable<Booking> book = await _da.Booking.GetAllAsync();
                if (book.Any(x => x.Schedule_Id.Equals(bk.Schedule_Id) && x.Customer_Id.Equals(bk.Customer_Id) && x.B_status.Equals("Yes") && x.Booking_date.Equals(bk.Booking_date)))
                {
                    return await Task.FromResult(0);
                }

                //check if the foreign key items exist in the primary key table
                else if(!(sh.Any(x=>x.Id.Equals(bk.Schedule_Id))) || !(cs.Any(x => x.Id.Equals(bk.Customer_Id))) || !(re.Any(x => x.Id.Equals(bk.Reward_Id))))
                {
                    return await Task.FromResult(1);
                }
                //else if(sh.Any(x=>x.Id)
                else
                {
                    /*var obs = new Booking();
                    obs.Booking_date = bk.Booking_date;
                    obs.B_status = bk.B_status;
                    obs.Schedule_Id = bk.Schedule_Id;
                    obs.Customer_Id = bk.Customer_Id;
                    obs.Reward_Id = bk.Reward_Id;*/
                    _da.Booking.AddAsync(bk);
                    _da.Save();
                    return await Task.FromResult(2);
                }

            }
            else
            {
                return await Task.FromResult(-1);
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