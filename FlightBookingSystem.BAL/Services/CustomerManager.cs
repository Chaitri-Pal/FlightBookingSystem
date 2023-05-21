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
    public class CustomerManager : ICustomerManager
    {
        private readonly IUnitOfWork _da;
        public CustomerManager(IUnitOfWork da) 
        {
            _da = da;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _da.Customer.GetAllAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _da.Customer.GetFirstorDefaultAsync(x=>x.Customer_Id==id);
        }

        public async Task<bool> AddCustomer(Customer cs)
        {
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
           if(cs != null)
            {
                IEnumerable<Customer> cust = await _da.Customer.GetAllAsync();
                if(cust.Any((x => x.C_Name.Equals(cs.C_Name) && x.Aadhar.Equals(cs.Aadhar))))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var cus = new Customer();
                    cus.C_Name = cs.C_Name;
                    cus.Address = cs.Address;
                    cus.Phone = cs.Phone;
                    cus.Email = cs.Email;
                    cus.Aadhar = cs.Aadhar;
                    cus.DOB = cs.DOB;
                    _da.Customer.AddAsync(cus);
                    _da.Save();
                    return await Task.FromResult(true);
                }
               
            }
           else 
            { 
                return false;
            }
        }

        public void UpdateCustomer(Customer cs)
        {
            _da.Customer.UpdateExisting(cs);
            _da.Save();
        }

        public void DeleteCustomer(Customer cs)
        {
            _da.Customer.Remove(cs);
            _da.Save();
        }
    }
}
