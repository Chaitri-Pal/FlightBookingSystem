using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Models
{
    public class Schedule
    {
        [Key]
        [Required]
        public int Schedule_Id { get; set; }
        [Required]

        public int Flight_Id { get; set; }
        [Required]
        public DateTime dep_time { get; set; }
        [Required]
        public DateTime arr_time { get; set; }
        [Required]
        
        public int dep_loc_id { get; set; }
        [Required]
        
        public int arr_loc_id { get; set; }
        
        public virtual Airport arrivalairport { get; set; }
        public virtual Airport departureairport { get; set; }
        
        [ForeignKey("Flight_Id")]
        public virtual ICollection<Flight> flight { get; set; }
        public virtual ICollection<Booking> booking { get; set; }
    }
}
