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
    public class AirportManager : IAirportManager
    {
        private readonly IUnitOfWork _da;
        public AirportManager(IUnitOfWork da)
        {
            _da = da;
        }
        public async Task<IEnumerable<Airport>> GetAllAirportsAsync()
        {
            return await _da.Airport.GetAllAsync();
        }

        public async Task<Airport> GetAirportAsync(int id)
        {
            return await _da.Airport.GetFirstorDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> AddAirport(Airport ai)
        {
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
            if (ai != null)
            {
                IEnumerable<Airport> air = await _da.Airport.GetAllAsync();
                if (air.Any(x => x.A_code.Equals(ai.A_code) && x.State.Equals(ai.State)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var obs = new Airport();
                    obs.A_code = ai.A_code;
                    obs.State = ai.State;
                    obs.City = ai.City;
                    _da.Airport.AddAsync(obs);
                    _da.Save();
                    return await Task.FromResult(true);
                }

            }
            else
            {
                return false;
            }
        }

        public void UpdateAirport(Airport ai)
        {
            _da.Airport.UpdateExisting(ai);
            _da.Save();
        }

        public void DeleteAirport(Airport ai)
        {
            _da.Airport.Remove(ai);
            _da.Save();
        }
    }
}