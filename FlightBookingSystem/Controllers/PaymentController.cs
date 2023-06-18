using AutoMapper;
using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;
using FlightBookingSystem.DAL.View_Model;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;
        public PaymentController(IPaymentManager py, IMapper mapper)
        {
            _py = py;
            _mapper = mapper;
        }



        //Get:api/Payment
        /// <summary>
        /// This method returns all the Payments from Payment Table
        /// </summary>
        /// <returns>List of all the Payments</returns>
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<PaymentVM>> GetPayments()
        {
            IEnumerable<Payment> pt = await _py.GetAllPaymentsAsync();
            var ptob = pt.Select(pt => _mapper.Map<PaymentVM>(pt));
            return ptob;
            //return await _py.GetAllPaymentsAsync();
        }




        //Get: api/Payment/{id}
        /// <summary>
        /// This method returns a single Payment that matches wit the Id parameter of a Payment
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Payment that matches with the id</returns>
        [HttpGet("{Id}")]
        //[Authorize]
        public async Task<PaymentVM> GetAPayment(int Id)
        {
            Payment pt = await _py.GetPaymentAsync(Id);
            var ptob = _mapper.Map<PaymentVM>(pt);
            return ptob;
            //return await _py.GetPaymentAsync(Id);
        }





        //Post: api/Payment
        /// <summary>
        /// This method adds new Payment objects to the Payment table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="py"></param>
        /// <returns>Output that Payment is added/exists/ or not</returns>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddPayment(PaymentVM py)
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
                    Payment ptob = _mapper.Map<Payment>(py);
                    var check = await _py.AddPayment(ptob);
                    if (check == -1)
                        return StatusCode(StatusCodes.Status400BadRequest, "The Payment object entered is empty"); 
                    else if (check == 0)
                        return StatusCode(StatusCodes.Status400BadRequest, "Payment already exists");
                    else if (check == 1)
                        return StatusCode(StatusCodes.Status400BadRequest, "Foreign Key values are not correct");
                    else
                        return StatusCode(StatusCodes.Status201Created, "New Payment is Created");
                    /*if (check == 2)
                        return StatusCode(StatusCodes.Status201Created, "New Payment is Created");
                    else if(check == 0)
                        return StatusCode(StatusCodes.Status400BadRequest, "Payment already exists");
                    else if (check == 1)
                        return StatusCode(StatusCodes.Status400BadRequest, "Foreign Key values are not correct");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "The Payment object entered is empty");*/
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
        //[Authorize]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentVM py)
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
                        /*existpy.Booking_Id = py.Booking_Id;
                        existpy.P_Type = py.P_Type;
                        existpy.P_Status = py.P_Status;
                        existpy.Payment_date = py.Payment_date;
                        existpy.Amount = py.Amount;
                        existpy.Customer_Id = py.Customer_Id;*/
                        Payment ptob = _mapper.Map<PaymentVM,Payment>(py,existpy);
                        _py.UpdatePayment(ptob);
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
        //[Authorize]
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
