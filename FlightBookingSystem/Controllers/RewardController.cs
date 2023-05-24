using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Controller should call BAL layer and BAL layer should call DAta Access Layer
    //Similar to DAL we'll write another repository for BAL and inject the both in controllers
    public class RewardController : ControllerBase
    {
        private readonly IRewardManager _rw;
        public RewardController(IRewardManager rw)
        {
            _rw = rw;
        }



        //Get:api/Reward
        /// <summary>
        /// This method returns all the Reward from Reward Table
        /// </summary>
        /// <returns>List of all the Reward</returns>
        [HttpGet]
        public async Task<IEnumerable<Reward>> GetRewards()
        {
            return await _rw.GetAllRewardsAsync();
        }




        //Get: api/Reward/{id}
        /// <summary>
        /// This method returns a single Reward that matches wit the Id parameter of a Reward
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Reward that matches with the id</returns>
        [HttpGet("{id}")]
        public async Task<Reward> GetAReward(int Id)
        {
            return await _rw.GetRewardAsync(Id);
        }





        //Post: api/Reward
        /// <summary>
        /// This method adds new Reward objects to the Reward table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="rw"></param>
        /// <returns>Output that Reward is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddReward(Reward rw)
        {
            try
            {
                if (rw == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    if (await _rw.AddReward(rw))
                        return StatusCode(StatusCodes.Status201Created, "New Reward is Created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Reward already exists");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new Reward");
            }
        }





        /// <summary>
        /// This method is used to update Reward details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rw"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReward(int id, [FromBody] Reward rw)
        {
            try
            {
                if (rw == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Reward existRw = await _rw.GetRewardAsync(id);
                    if (existRw == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    else
                    {
                        existRw.Loyalty_Value = rw.Loyalty_Value;
                        existRw.Discount = rw.Discount;
                        _rw.UpdateReward(existRw);
                        return Ok("Reward detail updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating Reward Details");
            }
        }




        /// <summary>
        /// This method is used to remove Reward details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deleted or not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReward(int id)
        {
            try
            {
                if (id == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Reward delRw = await _rw.GetRewardAsync(id);
                    if (delRw == null)
                    { return NotFound("ID does not exist"); }
                    _rw.DeleteReward(delRw);
                    return Ok("Reward Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Reward Details");
            }

        }


    }
}
