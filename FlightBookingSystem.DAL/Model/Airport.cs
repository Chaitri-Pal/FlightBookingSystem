using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Model
{
    public class Airport
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string A_code { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }


        //Navigation properties of child tables
        public virtual ICollection<Schedule> arrivalSchedule { get; set; }
        public virtual ICollection<Schedule> departureSchedule { get; set; }
    }
}
