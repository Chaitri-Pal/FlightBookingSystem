using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BAL.Services
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IUnitOfWork _da;
        public PaymentManager(IUnitOfWork da)
        {
            _da = da;
        }
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _da.Payment.GetAllAsync();
        }

        public async Task<Payment> GetPaymentAsync(int id)
        {
            return await _da.Payment.GetFirstorDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddPayment(Payment py)
        {
            IEnumerable<Customer> cs = await _da.Customer.GetAllAsync();
            IEnumerable<Booking> bk = await _da.Booking.GetAllAsync();
            IEnumerable<Reward> re = await _da.Reward.GetAllAsync();
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
            if (py != null)
            {
                IEnumerable<Payment> ptm = await _da.Payment.GetAllAsync();
                if (ptm.Any(x => x.Booking_Id.Equals(py.Booking_Id) && x.Customer_Id.Equals(py.Customer_Id) && x.P_Status.Equals("Yes")))
                {
                    //&& x.Customer_Id.Equals(py.Customer_Id)
                    return await Task.FromResult(0);
                }
                //check if the foreign key items exist in the primary key table
                else if (!(cs.Any(x => x.Id.Equals(py.Customer_Id))) || !(bk.Any(x => x.Id.Equals(py.Booking_Id))) || !(re.Any(x => x.Id.Equals(py.Reward_Id))))
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    //var obs = new Payment();
                    
                    /*obs.P_Type = py.P_Type;
                    obs.P_Status = py.P_Status;
                    obs.Payment_date = py.Payment_date;
                    obs.Amount = py.Amount;
                    obs.Customer_Id = py.Customer_Id;
                    obs.Reward_Id = py.Reward_Id;
                    obs.Booking_Id = py.Booking_Id;*/
                    _da.Payment.AddAsync(py);
                    _da.Save();
                    return await Task.FromResult(2);
                }

            }
            else
            {
                return -1;
            }
        }

        public void UpdatePayment(Payment py)
        {
            _da.Payment.UpdateExisting(py);
            _da.Save();
        }

        public void DeletePayment(Payment py)
        {
            _da.Payment.Remove(py);
            _da.Save();
        }
    }
}
