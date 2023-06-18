using AutoMapper;
using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;
using FlightBookingSystem.DAL.View_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

namespace FlightBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Controller should call BAL layer and BAL layer should call DAta Access Layer
    //Similar to DAL we'll write another repository for BAL and inject the both in controllers
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleManager _sh;
        private readonly IMapper _mapper;
        public ScheduleController(IScheduleManager sh, IMapper mapper)
        {
            _sh = sh;
            _mapper = mapper;
        }



        //Get:api/Schedule
        /// <summary>
        /// This method returns all the Schedules from Schedule Table
        /// </summary>
        /// <returns>List of all the Schedules</returns>
        [HttpGet]
        public async Task<IEnumerable<ScheduleVM>> GetSchedules()
        {
            IEnumerable<Schedule> shd = await _sh.GetAllSchedulesAsync();
            var shob = shd.Select(shd => _mapper.Map<ScheduleVM>(shd));
            return shob;
            //return await _sh.GetAllSchedulesAsync();
        }



        //Get:api/Schedule
        /// <summary>
        /// This method returns all the Schedules based on location and date
        /// </summary>
        /// <returns>List of all the Schedules</returns>
        [HttpGet("{sourceLocation}/{destinationLocation}/{date}")]
        public async Task<IEnumerable<ScheduleVM>> GetScheduleByRequirement(int sourceLocation, int destinationLocation, DateTime date)
        {
            IEnumerable<Schedule> shd = await _sh.GetRequiredSchedulesAsync(sourceLocation, destinationLocation, date);
            var shob = shd.Select(shd => _mapper.Map<ScheduleVM>(shd));
            return shob;
            //return await _sh.GetAllSchedulesAsync();
        }



        //Get: api/Schedule/{id}
        /// <summary>
        /// This method returns a single Schedule that matches wit the Id parameter of a Schedule
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Schedule that matches with the id</returns>
        [HttpGet("{Id}")]
        public async Task<ScheduleVM> GetASchedule(int Id)
        {
            Schedule shd = await _sh.GetScheduleAsync(Id);
            var shob = _mapper.Map<ScheduleVM>(shd);
            return shob;
            //return await _sh.GetScheduleAsync(Id);
        }





        //Post: api/Schedule
        /// <summary>
        /// This method adds new Schedule objects to the Schedule table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="sh"></param>
        /// <returns>Output that Schedule is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddSchedule(ScheduleVM sh)
        {
            try
            {
                if (sh == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Schedule shob = _mapper.Map<Schedule>(sh);
                    var check = await _sh.AddSchedule(shob);
                    if (check == 2)
                        return StatusCode(StatusCodes.Status201Created, "New Schedule is Created");
                    else if (check == 0)
                        return StatusCode(StatusCodes.Status400BadRequest, "Schedule already exists");
                    else if (check == 1)
                        return StatusCode(StatusCodes.Status400BadRequest, "Foreign Key values are wrong");
                    else if (check == -2)
                        return StatusCode(StatusCodes.Status400BadRequest, "Arrival and Departure Airports cannot be same");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "The Payment object entered is empty");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new Schedule");
            }
        }





        /// <summary>
        /// This method is used to update Schedule details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sh"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleVM sh)
        {
            try
            {
                if (sh == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Schedule existsh = await _sh.GetScheduleAsync(id);
                    if (existsh == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    else
                    {
                        /*existsh.Dep_Time = sh.Dep_Time;
                        existsh.Arr_Time = sh.Arr_Time;
                        existsh.Dep_id = sh.Dep_id;
                        existsh.Arr_id = existsh.Arr_id;
                        existsh.Flight_Id = sh.Flight_Id;*/
                        Schedule shob = _mapper.Map<ScheduleVM,Schedule>(sh, existsh);
                        _sh.UpdateSchedule(existsh);
                        return Ok("Schedule detail updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating Schedule Details");
            }
        }




        /// <summary>
        /// This method is used to remove Schedule details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deleted or not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
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
                    Schedule delsh = await _sh.GetScheduleAsync(id);
                    if (delsh == null)
                    { return NotFound("ID does not exist"); }
                    _sh.DeleteSchedule(delsh);
                    return Ok("Schedule Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Schedule Details");
            }

        }


    }
}
