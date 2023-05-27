using AutoMapper;
using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;
using FlightBookingSystem.DAL.View_Model;
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
        private readonly IMapper _mapper;
        public BookingController(IBookingManager bk, IMapper mapper)
        {
            _bk = bk;
            _mapper = mapper;
        }



        //Get:api/Booking
        /// <summary>
        /// This method returns all the Bookings from Booking Table
        /// </summary>
        /// <returns>List of all the Bookings</returns>
        [HttpGet]
        public async Task<IEnumerable<BookingVM>> GetBookings()
        {
            IEnumerable<Booking> book = await _bk.GetAllBookingsAsync();
            var bobj = book.Select(book=>_mapper.Map<BookingVM>(book));
            return bobj;

            /*List<BookingVM> bkv = new List<BookingVM>();
            var book = await _bk.GetAllBookingsAsync();
            foreach (Booking i in book)
            {
                BookingVM b = new BookingVM();
                b.Booking_date = i.Booking_date;
                b.B_status = i.B_status;
                b.Customer_Id = i.Customer_Id;
                b.Schedule_Id = i.Schedule_Id;
                b.Reward_Id = i.Reward_Id;
                bkv.Add(b);
            }
            return bkv;*/
        
        //return await _bk.GetAllBookingsAsync();
    }




        //Get: api/Booking/{id}
        /// <summary>
        /// This method returns a single Booking that matches wit the Id parameter of a Booking
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Booking that matches with the id</returns>
        [HttpGet("{Id}")]
        public async Task<BookingVM> GetABooking(int Id)
        {
            Booking bkv = await _bk.GetBookingAsync(Id);
            var obj = _mapper.Map<BookingVM>(bkv);
            return obj;

            //return await _bk.GetBookingAsync(Id);
        }





        //Post: api/Booking
        /// <summary>
        /// This method adds new Booking objects to the Booking table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="bk"></param>
        /// <returns>Output that Booking is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddBooking(BookingVM bk)
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
                    Booking bob = _mapper.Map<Booking>(bk);
                    var check = await _bk.AddBooking(bob);
                    if (check == 0)
                        return StatusCode(StatusCodes.Status400BadRequest, "Booking already exists");
                    else if (check == 1)
                        return StatusCode(StatusCodes.Status400BadRequest, "Foreign key values are not correct");

                    else if(check == -1)
                        return StatusCode(StatusCodes.Status400BadRequest, "The Booking object entered is empty");
                    else 
                        return StatusCode(StatusCodes.Status201Created, "New Booking created");

                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error while Adding new Booking");
            }
        }





        /// <summary>
        /// This method is used to update Booking details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bk"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingVM bk)
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
                        var bookobj = _mapper.Map<BookingVM, Booking>(bk,existBk);
                        /*existBk.Schedule_Id = bk.Schedule_Id;
                        existBk.Customer_Id = bk.Customer_Id;
                        existBk.Booking_date = bk.Booking_date;                
                        existBk.B_status = bk.B_status;
                        existBk.Reward_Id = bk.Reward_Id;*/
                        _bk.UpdateBooking(bookobj);
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
