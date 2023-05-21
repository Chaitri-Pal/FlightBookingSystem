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
    public class RewardManager : IRewardManager
    {
        private readonly IUnitOfWork _da;
        public RewardManager(IUnitOfWork da)
        {
            _da = da;
        }
        public async Task<IEnumerable<Reward>> GetAllRewardsAsync()
        {
            return await _da.Reward.GetAllAsync();
        }

        public async Task<Reward> GetRewardAsync(int id)
        {
            return await _da.Reward.GetFirstorDefaultAsync(x => x.Reward_Id == id);
        }

        public async Task<bool> AddReward(Reward rw)
        {
            //if input is not null then the value is checked if it already exists or not if it does hen return already exists else add it
            if (rw != null)
            {
                IEnumerable<Reward> rew = await _da.Reward.GetAllAsync();
                if (rew.Any((x => x.loyalty_value.Equals(rw.loyalty_value) && x.Discount.Equals(rw.Discount))))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var obs = new Reward();
                    obs.loyalty_value = rw.loyalty_value;
                    obs.Discount = rw.Discount;
                    _da.Reward.AddAsync(obs);
                    _da.Save();
                    return await Task.FromResult(true);
                }

            }
            else
            {
                return false;
            }
        }

        public void UpdateReward(Reward cs)
        {
            _da.Reward.UpdateExisting(cs);
            _da.Save();
        }

        public void DeleteReward(Reward cs)
        {
            _da.Reward.Remove(cs);
            _da.Save();
        }
    }
}
