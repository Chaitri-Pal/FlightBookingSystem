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
    public class ScheduleManager : IScheduleManager
    {
        private readonly IUnitOfWork _da;
        public ScheduleManager(IUnitOfWork da)
        {
            _da = da;
        }
        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            return await _da.Schedule.GetAllAsync();
        }

        public async Task<Schedule> GetScheduleAsync(int id)
        {
            return await _da.Schedule.GetFirstorDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> AddSchedule(Schedule sh)
        {
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
            if (sh != null)
            {
                IEnumerable<Schedule> sch = await _da.Schedule.GetAllAsync();
                //&& x.Flight_Id.Equals(sh.Flight_Id)
                if (sch.Any(x => x.Dep_Time.Equals(sh.Dep_Time) && x.Arr_Time.Equals(sh.Arr_Time)))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var scd = new Schedule();
                    scd.Dep_Time = sh.Dep_Time;
                    scd.Arr_Time = sh.Arr_Time;
                    scd.Dep_id = sh.Dep_id;
                    scd.Arr_id = scd.Arr_id;
                    //scd.Flight_ID = sh.Flight_ID;
                    _da.Schedule.AddAsync(scd);
                    _da.Save();
                    return await Task.FromResult(true);
                }

            }
            else
            {
                return false;
            }
        }

        public void UpdateSchedule(Schedule sh)
        {
            _da.Schedule.UpdateExisting(sh);
            _da.Save();
        }

        public void DeleteSchedule(Schedule sh)
        {
            _da.Schedule.Remove(sh);
            _da.Save();
        }
    }
}
