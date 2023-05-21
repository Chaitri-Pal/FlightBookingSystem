using FlightBookingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BAL.Contacts
{
    public interface ICustomerManager
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<bool> AddCustomer(Customer cs);
        void UpdateCustomer(Customer cs);
    }
}
