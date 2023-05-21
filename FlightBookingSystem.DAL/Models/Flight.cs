using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Models
{
    public class Flight
    {
        [Key]
        [Required]
        public int Flight_ID { get; set; }
        [Required]
        public string Flight_Name { get; set; }
        [Required]
        public int Seat_Capacity { get; set; }
        [Required]
        public int Vacant_Seats { get; set; }
        [Required]
        public int Weight_limit { get; set; }
        [Required]
        public int Flying_hours { get; set; }
        public virtual ICollection<Schedule> schedules { get; set; }
    }
}
