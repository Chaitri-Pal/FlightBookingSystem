using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;

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

        public async Task<int> AddSchedule(Schedule sh)
        {
            IEnumerable<Airport> ai = await _da.Airport.GetAllAsync();
            IEnumerable<Flight> fl = await _da.Flight.GetAllAsync();
            
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
            if (sh != null)
            {
                IEnumerable<Schedule> sch = await _da.Schedule.GetAllAsync();
                //&& x.Flight_Id.Equals(sh.Flight_Id)
                if (sch.Any(x => x.Dep_Time.Equals(sh.Dep_Time) && x.Arr_Time.Equals(sh.Arr_Time) && x.Arr_id.Equals(sh.Arr_id) && x.Dep_id.Equals(sh.Dep_id)))
                {
                    return await Task.FromResult(0);
                }

                else if (!(ai.Any(x => x.Id.Equals(sh.Dep_id))) || !(ai.Any(x=> x.Id.Equals(sh.Arr_id))) || !(fl.Any(x => x.Id.Equals(sh.Flight_Id))))
                {
                    return await Task.FromResult(1);
                }
                else
                {
                    if(!(sh.Arr_id.Equals(sh.Dep_id)))
                    {
                        _da.Schedule.AddAsync(sh);
                        _da.Save();
                        return await Task.FromResult(2);
                    }
                    /*var scd = new Schedule();
                    scd.Dep_Time = sh.Dep_Time;
                    scd.Arr_Time = sh.Arr_Time;
                    scd.Dep_id = sh.Dep_id;
                    scd.Arr_id = scd.Arr_id;
                    scd.Flight_Id = sh.Flight_Id;*/
                    return -2;
                }

            }
            else
            {
                return -1;
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

        public async Task<IEnumerable<Schedule>> GetRequiredSchedulesAsync(int src, int dest, DateTime date)
        {
            return await _da.Schedule.GetAllListAsync(x => x.Arr_id == src && x.Dep_id == dest && (x.Arr_Time.Date).Equals(date) && (x.Dep_Time.Date).Equals(date));
        }
    }
}
