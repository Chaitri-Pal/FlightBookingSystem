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
    public class UserController : ControllerBase
    {
        private readonly IUserManager _cm;
        private readonly IMapper _mapper;
        public UserController(IUserManager cm, IMapper mapper)
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
        public async Task<IEnumerable<UserVM>> GetCustomers()
        {
            IEnumerable<User> customers = new List<User>();
            customers = await _cm.GetAllUsersAsync();
            var cu = customers.Select(customers => _mapper.Map<UserVM>(customers));
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
        public async Task<UserVM> GetAUser(int Id)
        {
            //return await _cm.GetCustomerAsync(Id);
            User cs = await _cm.GetUserAsync(Id);
            var c = _mapper.Map<UserVM>(cs);
            return c;

        }





        //Post: api/Customer
        /// <summary>
        /// This method adds new customer objects to the Customer table and give appropriate outputs in case of errors.
        /// </summary>
        /// <param name="cs"></param>
        /// <returns>Output that customer is added/exists/ or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddUser(UserVM cs)
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
                    User cus = _mapper.Map<User>(cs);
                    
                    if(await _cm.AddUser(cus))
                        return StatusCode(StatusCodes.Status201Created, "New User is Created");
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "User already exists");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new User");
            }
        }





        /// <summary>
        /// This method is used to update customer details by giving the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cs"></param>
        /// <returns>Updated or not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,[FromBody] UserVM cs)
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
                    User existCs = await _cm.GetUserAsync(id);
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
                        User cust = _mapper.Map<UserVM,User>(cs, existCs);

                        _cm.UpdateUser(cust);
                       
                        return Ok("User detail updated ");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Updating User Details");
            }
        }




        /// <summary>
        /// This method is used to remove customer details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>deleted or not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
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
                    User delCs = await _cm.GetUserAsync(id);
                    if (delCs == null)
                    { return NotFound("ID does not exist"); }
                    _cm.DeleteUser(delCs);
                    return Ok("User Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Removing User Details");
            }

        }


    }
}
