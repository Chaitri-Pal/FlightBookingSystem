using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Controller should call BAL layer and BAL layer should call DAta Access Layer
    //Similar to DAL we'll write another repository for BAL and inject the both in controllers
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager _bk;
        public BookingController(IBookingManager bk)
        {
            _bk = bk;
        }



        //Get:api/Booking
        /// <summary>
        /// This method returns all the Bookings from Booking Table
        /// </summary>
        /// <returns>List of all the Bookings</returns>
        [HttpGet]
        public async Task<IEnumerable<Booking>> GetBookings()
        {
            return await _bk.GetAllBookingsAsync();
        }




        //Get: api/Booking/{id}
        /// <summary>
        /// This method returns a single Booking that matches wit the Id parameter of a Booking
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Booking that matches with the id</returns>
        [HttpGet("{id}")]
        public async Task<Booking> GetABooking(int Id)
        {
            return await _bk.GetBookingAsync(Id);
        }





        //Post: api/Booking
        /// <summary>
        /// This method adds new Booking objects to the Booking table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="bk"></param>
        /// <returns>Output that Booking is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddBooking(Booking bk)
        {
            try
            {
                if (bk == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    if (await _bk.AddBooking(bk))
                        return StatusCode(StatusCodes.Status201Created, "New Booking is Created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Booking already exists");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new Booking");
            }
        }





        /// <summary>
        /// This method is used to update Booking details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bk"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] Booking bk)
        {
            try
            {
                if (bk == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Booking existBk = await _bk.GetBookingAsync(id);
                    if (existBk == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    else
                    {
                        existBk.Schedule_Id = bk.Schedule_Id;
                        existBk.Customer_Id = bk.Customer_Id;
                        existBk.Booking_date = bk.Booking_date;                
                        existBk.B_status = bk.B_status;

                        //existBk.Reward_Id = bk.Reward_Id;
                        
                        return Ok("Booking detail updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating Booking Details");
            }
        }




        /// <summary>
        /// This method is used to remove customer details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deleted or not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
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
                    Booking delbk = await _bk.GetBookingAsync(id);
                    if (delbk == null)
                    { return NotFound("ID does not exist"); }
                    _bk.DeleteBooking(delbk);
                    return Ok("Booking Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Booking Details");
            }

        }


    }
}
