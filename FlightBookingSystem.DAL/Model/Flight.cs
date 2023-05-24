using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Model
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Flight_Name { get; set; }
        [Required]
        public int Seat_Capacity { get; set; }
        [Required]
        public int Vacant_Seat { get; set; }
        [Required]
        public int Weight_Limit { get; set; }
        [Required]
        public int Flying_Hours { get; set; }


        //Navigation properties of child tables
        public virtual ICollection<Schedule> schedules { get; set; }

    }
}
