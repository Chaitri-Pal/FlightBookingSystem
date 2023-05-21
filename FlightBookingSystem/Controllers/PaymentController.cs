using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Controller should call BAL layer and BAL layer should call DAta Access Layer
    //Similar to DAL we'll write another repository for BAL and inject the both in controllers
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentManager _py;
        public PaymentController(IPaymentManager py)
        {
            _py = py;
        }



        //Get:api/Payment
        /// <summary>
        /// This method returns all the Payments from Payment Table
        /// </summary>
        /// <returns>List of all the Payments</returns>
        [HttpGet]
        public async Task<IEnumerable<Payment>> GetPayments()
        {
            return await _py.GetAllPaymentsAsync();
        }




        //Get: api/Payment/{id}
        /// <summary>
        /// This method returns a single Payment that matches wit the Id parameter of a Payment
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Payment that matches with the id</returns>
        [HttpGet("{id}")]
        public async Task<Payment> GetAPayment(int Id)
        {
            return await _py.GetPaymentAsync(Id);
        }





        //Post: api/Payment
        /// <summary>
        /// This method adds new Payment objects to the Payment table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="py"></param>
        /// <returns>Output that Payment is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddPayment(Payment py)
        {
            try
            {
                if (py == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    if (await _py.AddPayment(py))
                        return StatusCode(StatusCodes.Status201Created, "New Payment is Created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Payment already exists");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new Payment");
            }
        }





        /// <summary>
        /// This method is used to update Payment details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="py"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment py)
        {
            try
            {
                if (py == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Payment existpy = await _py.GetPaymentAsync(id);
                    if (existpy == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    else
                    {
                        existpy.Booking_Id = py.Booking_Id;
                        existpy.P_type = py.P_type;
                        existpy.P_status = py.P_status;
                        existpy.Payment_date = py.Payment_date;
                        existpy.Amount = py.Amount;
                        //existpy.Customer_Id = py.Customer_Id;
                        _py.UpdatePayment(existpy);
                        return Ok("Payment detail updated");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating Payment Details");
            }
        }




        /// <summary>
        /// This method is used to remove Payment details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deleted or not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
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
                    Payment delpy = await _py.GetPaymentAsync(id);
                    if (delpy == null)
                    { return NotFound("ID does not exist"); }
                    _py.DeletePayment(delpy);
                    return Ok("Payment Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Payment Details");
            }

        }


    }
}
