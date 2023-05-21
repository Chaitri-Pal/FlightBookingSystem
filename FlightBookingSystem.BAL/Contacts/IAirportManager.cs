using FlightBookingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BAL.Contacts
{
    public interface IAirportManager
    {
        Task<IEnumerable<Airport>> GetAllAirportsAsync();
        Task<Airport> GetAirportAsync(int id);
        Task<bool> AddAirport(Airport ai);
        void UpdateAirport(Airport ai);
        void DeleteAirport(Airport ai);
    }
}