using FlightBookingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BAL.Contacts
{
    public interface IRewardManager
    {
        Task<IEnumerable<Reward>> GetAllRewardsAsync();
        Task<Reward> GetRewardAsync(int id);
        Task<bool> AddReward(Reward rw);
        void UpdateReward(Reward rw);
        void DeleteReward(Reward rw);
    }
}
