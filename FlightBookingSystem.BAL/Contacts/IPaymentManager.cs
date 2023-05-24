using FlightBookingSystem.DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BAL.Contacts
{
    public interface IPaymentManager
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentAsync(int id);
        Task<bool> AddPayment(Payment py);
        void UpdatePayment(Payment py);
        void DeletePayment(Payment py);
    }
}
