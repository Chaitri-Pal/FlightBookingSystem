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
        public int Flight_Id { get; set; }
        [Required]
        public string Flight_Name { get; set; }
        [Required]
        public int Seat_capacity { get; set; }
        [Required]
        public int VacantSeat_capacity { get; set; }
        [Required]
        public int Weight_limit { get; set; }
        [Required]
        public int Flying_hours { get; set; }
        [Required]
        public virtual Schedule schedule { get; set; }
    }
}
