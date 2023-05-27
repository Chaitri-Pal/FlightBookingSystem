using FlightBookingSystem.DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BAL.Contacts
{
    public interface IScheduleManager
    {
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<Schedule> GetScheduleAsync(int id);
        Task<int> AddSchedule(Schedule sh);
        void UpdateSchedule(Schedule sh);
        void DeleteSchedule(Schedule sh);
    }
}
