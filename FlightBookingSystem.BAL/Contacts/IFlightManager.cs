using FlightBookingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BAL.Contacts
{
    public interface IFlightManager
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightAsync(int id);
        Task<bool> AddFlight(Flight fl);
        void UpdateFlight(Flight fl);
        void DeleteFlight(Flight fl);
    }
}
