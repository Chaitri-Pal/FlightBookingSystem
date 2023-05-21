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
    public class FlightManager : IFlightManager
    {
        private readonly IUnitOfWork _da;
        public FlightManager(IUnitOfWork da)
        {
            _da = da;
        }
        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _da.Flight.GetAllAsync();
        }

        public async Task<Flight> GetFlightAsync(int id)
        {
            return await _da.Flight.GetFirstorDefaultAsync(x => x.Flight_ID == id);
        }

        public async Task<bool> AddFlight(Flight fl)
        {
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
            if (fl != null)
            {
                IEnumerable<Flight> flt = await _da.Flight.GetAllAsync();
                if (flt.Any((x => x.Flight_Name.Equals(fl.Flight_Name) && x.Seat_Capacity.Equals(fl.Seat_Capacity))))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var flg = new Flight();
                    flg.Flight_Name = fl.Flight_Name;
                    flg.Seat_Capacity = fl.Seat_Capacity;
                    flg.Vacant_Seats = fl.Vacant_Seats;
                    flg.Weight_limit = fl.Weight_limit;
                    flg.Flying_hours = fl.Flying_hours;
                    _da.Flight.AddAsync(flg);
                    _da.Save();
                    return await Task.FromResult(true);
                }

            }
            else
            {
                return false;
            }
        }

        public void UpdateFlight(Flight fl)
        {
            _da.Flight.UpdateExisting(fl);
            _da.Save();
        }

        public void DeleteFlight(Flight fl)
        {
            _da.Flight.Remove(fl);
            _da.Save();
        }
    }
}
