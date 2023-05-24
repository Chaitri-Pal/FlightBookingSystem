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
    public class AirportController : ControllerBase
    {
        private readonly IAirportManager _am;
        public AirportController(IAirportManager am)
        {
            _am = am;
        }



        //Get:api/Airport
        /// <summary>
        /// This method returns all the Airports from Airport Table
        /// </summary>
        /// <returns>List of all the Airport</returns>
        [HttpGet]
        public async Task<IEnumerable<Airport>> GetAirports()
        {
            return await _am.GetAllAirportsAsync();
        }




        //Get: api/Airport/{id}
        /// <summary>
        /// This method returns a single Airport that matches wit the Id parameter of a customer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Airport that matches with the id</returns>
        [HttpGet("{id}")]
        public async Task<Airport> GetAAirport(int Id)
        {
            return await _am.GetAirportAsync(Id);
        }





        //Post: api/Airport
        /// <summary>
        /// This method adds new Airport objects to the Airport table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="ai"></param>
        /// <returns>Output that Airport is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddAirport(Airport ai)
        {
            try
            {
                if (ai == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    if (await _am.AddAirport(ai))
                        return StatusCode(StatusCodes.Status201Created, "New Airport is Created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Airport already exists");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new Airport");
            }
        }





        /// <summary>
        /// This method is used to update customer details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ai"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAirport(int id, [FromBody] Airport ai)
        {
            try
            {
                if (ai == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Airport existAi = await _am.GetAirportAsync(id);
                    if (existAi == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    else
                    {
                        existAi.A_code = ai.A_code;
                        existAi.State = ai.State;
                        existAi.City = ai.City;
                        _am.UpdateAirport(existAi);
                        return Ok("Airport detail updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating Airport Details");
            }
        }




        /// <summary>
        /// This method is used to remove Airport details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deleted or not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(int id)
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
                    Airport delAi = await _am.GetAirportAsync(id);
                    if (delAi == null)
                    { return NotFound("ID does not exist"); }
                    _am.DeleteAirport(delAi);
                    return Ok("Airport Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Airport Details");
            }

        }


    }
}