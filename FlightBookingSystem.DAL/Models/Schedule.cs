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
        public DateTime Dep_Time { get; set; }
        [Required]
        public DateTime Arr_Time { get; set; }
        [Required]
        public int Dep_id { get; set; }
        [ForeignKey("Dep_id")]
        public virtual Airport departureAirport { get; set; }
        [Required]
        public int Arr_id { get; set; }
        [ForeignKey("Arr_id")]
        public virtual Airport arrivalAirport { get; set; }
  

    }
}
