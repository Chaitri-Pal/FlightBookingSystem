using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.View_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightBookingSystem.DAL.Model;
using FlightBookingSystem.DAL.View_Model;
using System.Reflection.Metadata.Ecma335;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;

namespace FlightBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        ApplicationDBContext _db;
        private IConfiguration _config;
        private readonly IMapper _mapper;

        public UserLoginController(IConfiguration config, ApplicationDBContext db, IMapper mapper)
        {
            _config = config;
            _db = db;
            _mapper = mapper;
        }


        [HttpPost("[action]")]
        public IActionResult Register([FromBody] UserVM user)
        {
            //checks whether the user email in register method is already present in database or not
            var userExists = _db.Users.FirstOrDefault(u => u.Email == user.Email);
            var c = _mapper.Map<User>(user);
            if (userExists == null)
            {
                _db.Users.Add(c);
                _db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest("User with same Email Id already exists!");
            }
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] UserLoginVM user)
        {
            var currentUser = _db.Users.FirstOrDefault(u=>u.Email == user.Email && u.Password == user.Password);
            if(currentUser == null)
            {
                return NotFound();
            }
          var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
          var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
              new Claim(ClaimTypes.Email, user.Email),
          };
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
           );
            //return token in string format
           var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwt);
        }


    }
}
