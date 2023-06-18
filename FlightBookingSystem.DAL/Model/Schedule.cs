using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Model
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Dep_Time { get; set; }
        [Required]
        public DateTime Arr_Time { get; set; }


        //Foreign Keys
        [Required]
        public int Dep_id { get; set; }
        [Required]
        public int Arr_id { get; set; }
        [Required]
        public int Flight_Id { get; set; }




        //Navigation properties of child/parent  tables
        public virtual Airport arrivalAirport {get; set;}
        public virtual Airport departureAirport { get; set; }
        public virtual Flight flights { get; set; }
        public virtual ICollection<Booking> bookings { get; set; }
    }
}
