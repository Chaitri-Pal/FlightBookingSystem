using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.View_Model
{
    public class AirportVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string A_code { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }


    }
}
