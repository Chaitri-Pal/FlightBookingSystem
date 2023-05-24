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
    public class FlightController : ControllerBase
    {
        private readonly IFlightManager _fl;
        public FlightController(IFlightManager fl)
        {
            _fl = fl;
        }



        //Get:api/Flight
        /// <summary>
        /// This method returns all the Flights from Flight Table
        /// </summary>
        /// <returns>List of all the Flights</returns>
        [HttpGet]
        public async Task<IEnumerable<Flight>> GetFlights()
        {
            return await _fl.GetAllFlightsAsync();
        }




        //Get: api/Flight/{id}
        /// <summary>
        /// This method returns a single Flight that matches wit the Id parameter of a Flight
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Flight that matches with the id</returns>
        [HttpGet("{id}")]
        public async Task<Flight> GetAFlight(int Id)
        {
            return await _fl.GetFlightAsync(Id);
        }





        //Post: api/Flight
        /// <summary>
        /// This method adds new Flight objects to the Flight table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="fl"></param>
        /// <returns>Output that Flight is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddFlight(Flight fl)
        {
            try
            {
                if (fl == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    if (await _fl.AddFlight(fl))
                        return StatusCode(StatusCodes.Status201Created, "New Flight is Created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Flight already exists");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new Flight");
            }
        }





        /// <summary>
        /// This method is used to update Flight details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fl"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] Flight fl)
        {
            try
            {
                if (fl == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Flight existFl = await _fl.GetFlightAsync(id);
                    if (existFl == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    else
                    {
                        existFl.Flight_Name = fl.Flight_Name;
                        existFl.Seat_Capacity = fl.Seat_Capacity;
                        existFl.Vacant_Seat = fl.Vacant_Seat;
                        existFl.Weight_Limit = fl.Weight_Limit;
                        existFl.Flying_Hours = fl.Flying_Hours;
                        _fl.UpdateFlight(existFl);
                        return Ok("Flight detail updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating Flight Details");
            }
        }




        /// <summary>
        /// This method is used to remove Flight details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deleted or not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
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
                    Flight delFl = await _fl.GetFlightAsync(id);
                    if (delFl == null)
                    { return NotFound("ID does not exist"); }
                    _fl.DeleteFlight(delFl);
                    return Ok("Flight Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Flight Details");
            }

        }


    }
}
