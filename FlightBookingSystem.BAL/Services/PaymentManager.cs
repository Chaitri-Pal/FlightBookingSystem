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
            return await _da.Payment.GetFirstorDefaultAsync(x => x.Payment_Id == id);
        }

        public async Task<bool> AddPayment(Payment py)
        {
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
            if (py != null)
            {
                IEnumerable<Payment> ptm = await _da.Payment.GetAllAsync();
                if (ptm.Any(x => x.Booking_Id.Equals(py.Booking_Id)))
                {
                    //&& x.Customer_Id.Equals(py.Customer_Id)
                    return await Task.FromResult(false);
                }
                else
                {
                    var obs = new Payment();
                    obs.Booking_Id = py.Booking_Id;
                    obs.P_type = py.P_type;
                    obs.P_status = py.P_status;
                    obs.Payment_date = py.Payment_date;
                    obs.Amount = py.Amount;
                    //obs.Customer_Id = py.Customer_Id;
                    _da.Payment.AddAsync(obs);
                    _da.Save();
                    return await Task.FromResult(true);
                }

            }
            else
            {
                return false;
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
