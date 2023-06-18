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
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _da;
        public UserManager(IUnitOfWork da) 
        {
            _da = da;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _da.User.GetAllAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _da.User.GetFirstorDefaultAsync(x=>x.Id==id);
        }

        public async Task<bool> AddUser(User cs)
        {
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
           if(cs != null)
            {
                IEnumerable<User> cust = await _da.User.GetAllAsync();
                if(cust.Any(x => x.Name.Equals(cs.Name) && x.Aadhar.Equals(cs.Aadhar) && x.Email.Equals(cs.Email)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var cus = new User();
                    /*cus.C_Name = cs.C_Name;
                    cus.Address = cs.Address;
                    cus.Phone = cs.Phone;
                    cus.Email = cs.Email;
                    cus.Aadhar = cs.Aadhar;
                    cus.DOB = cs.DOB;*/
                    _da.User.AddAsync(cs);
                    _da.Save();
                    return await Task.FromResult(true);
                }
               
            }
           else 
            { 
                return false;
            }
        }

        public void UpdateUser(User cs)
        {
            _da.User.UpdateExisting(cs);
            
            _da.Save();
        }

        public void DeleteUser(User cs)
        {
            _da.User.Remove(cs);
            _da.Save();
        }
    }
}
