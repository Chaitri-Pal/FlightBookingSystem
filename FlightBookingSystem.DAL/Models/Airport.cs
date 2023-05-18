using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Models
{
    public class Airport
    {
        [Key]
        [Required]
        public int Airport_Id { get; set; }
        [Required]
        public string A_code { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
       public virtual ICollection<Schedule> arrivalschedule { get; set; }
       public virtual ICollection<Schedule> departureschedule { get; set; }
        
    }
}
