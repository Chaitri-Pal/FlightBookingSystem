using AutoMapper;
using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;
using FlightBookingSystem.DAL.View_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FlightBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Controller should call BAL layer and BAL layer should call DAta Access Layer
    //Similar to DAL we'll write another repository for BAL and inject the both in controllers
    public class AirportController : ControllerBase
    {
        private readonly IAirportManager _am;
        private readonly IMapper _mapper;
        public AirportController(IAirportManager am, IMapper mapper)
        {
            _am = am;
            _mapper = mapper;
        }



        //Get:api/Airport
        /// <summary>
        /// This method returns all the Airports from Airport Table
        /// </summary>
        /// <returns>List of all the Airport</returns>
        [HttpGet]
        public async Task<IEnumerable<AirportVM>> GetAirports()
        {

            //return await _am.GetAllAirportsAsync();
            //List<AirportVM> avm = new List<AirportVM>();
                        
            IEnumerable<Airport> airp = await _am.GetAllAirportsAsync();
            var avm = airp.Select(airp=>_mapper.Map<AirportVM>(airp));
            return avm;


            /*foreach (Airport airport in airp)
            {
                AirportVM airportVM = new AirportVM();
                airportVM.A_code = airport.A_code;
                airportVM.State = airport.State;
                airportVM.City = airport.City;
                avm.Add(airportVM);
            }*/

        }




        //Get: api/Airport/{id}
        /// <summary>
        /// This method returns a single Airport that matches wit the Id parameter of a customer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Airport that matches with the id</returns>
        [HttpGet("{Id}")]
        public async Task<AirportVM> GetAAirport(int Id)
        {

            /*Airport ap = await _am.GetAirportAsync(Id);
            AirportVM avm = new AirportVM();
            avm.A_code = ap.A_code;
            avm.State = ap.State;
            avm.City = ap.City;
            return avm;*/
            Airport av = await _am.GetAirportAsync(Id);
            var airobj = _mapper.Map<AirportVM>(av);
            return airobj;


            //return await _am.GetAirportAsync(Id);
        }





        //Post: api/Airport
        /// <summary>
        /// This method adds new Airport objects to the Airport table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="ai"></param>
        /// <returns>Output that Airport is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddAirport(AirportVM ai)
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
                    Airport air = _mapper.Map<Airport>(ai);
                    if (await _am.AddAirport(air))
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
        public async Task<IActionResult> UpdateAirport(int id, [FromBody] AirportVM ai)
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
                        /*existAi.A_code = ai.A_code;
                        existAi.State = ai.State;
                        existAi.City = ai.City;*/
                        Airport airt = _mapper.Map<AirportVM, Airport>(ai, existAi);
                        _am.UpdateAirport(airt);
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