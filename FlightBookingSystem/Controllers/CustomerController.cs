using AutoMapper;
using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess.Interface;
using FlightBookingSystem.DAL.Model;
using FlightBookingSystem.DAL.View_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace FlightBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Controller should call BAL layer and BAL layer should call DAta Access Layer
    //Similar to DAL we'll write another repository for BAL and inject the both in controllers
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _cm;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerManager cm, IMapper mapper)
        {
            _cm = cm;
            _mapper = mapper;
        }



        //Get:api/Customer
        /// <summary>
        /// This method returns all the Customers from Customer Table
        /// </summary>
        /// <returns>List of all the Customers</returns>
        [HttpGet]
        public async Task<IEnumerable<CustomerVM>> GetCustomers()
        {
            IEnumerable<Customer> customers = new List<Customer>();
            customers = await _cm.GetAllCustomersAsync();
            var cu = customers.Select(customers => _mapper.Map<CustomerVM>(customers));
            return cu;
            //return await _cm.GetAllCustomersAsync();
        }




        //Get: api/Customer/{id}
        /// <summary>
        /// This method returns a single Customer that matches wit the Id parameter of a customer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>The Customer that matches with the id</returns>
        [HttpGet("{Id}")]
        public async Task<CustomerVM> GetACustomer(int Id)
        {
            //return await _cm.GetCustomerAsync(Id);
            Customer cs = await _cm.GetCustomerAsync(Id);
            var c = _mapper.Map<CustomerVM>(cs);
            return c;

        }





        //Post: api/Customer
        /// <summary>
        /// This method adds new customer objects to the Customer table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="cs"></param>
        /// <returns>Output that customer is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerVM cs)
        {
            try
            {
                if(cs == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Customer cus = _mapper.Map<Customer>(cs);
                    
                    if(await _cm.AddCustomer(cus))
                        return StatusCode(StatusCodes.Status201Created, "New Customer is Created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "Customer already exists");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new Customer");
            }
        }





        /// <summary>
        /// This method is used to update customer details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cs"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id,[FromBody] CustomerVM cs)
        {
            try
            {
                if (cs == null)
                {
                    //Function part of controllerbase
                    return BadRequest();
                }
                else
                {
                    Customer existCs = await _cm.GetCustomerAsync(id);
                    if (existCs == null)
                    {
                        return NotFound("Id does not exist");
                    }
                    else
                    {
                        /*existCs.C_Name = cs.C_Name;
                        existCs.Address = cs.Address;
                        existCs.Email = cs.Email;
                        existCs.Phone = cs.Phone;
                        existCs.Aadhar = cs.Aadhar;
                        existCs.DOB = cs.DOB;*/
                        Customer cust = _mapper.Map<CustomerVM,Customer>(cs, existCs);

                        _cm.UpdateCustomer(cust);
                       
                        return Ok("Customer detail updated ");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating Customer Details");
            }
        }




        /// <summary>
        /// This method is used to remove customer details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deleted or not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
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
                    Customer delCs = await _cm.GetCustomerAsync(id);
                    if (delCs == null)
                    { return NotFound("ID does not exist"); }
                    _cm.DeleteCustomer(delCs);
                    return Ok("Customer Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing Customer Details");
            }

        }


    }
}
