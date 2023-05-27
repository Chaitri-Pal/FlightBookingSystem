using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.View_Model
{
    public class CustomerVM
    {
        [Required]
        public int Id { get; set; }
        public string C_Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Aadhar { get; set; }
        [Required]
        public DateTime DOB { get; set; }
    }
}
