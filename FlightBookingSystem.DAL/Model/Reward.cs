using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Model
{
    public class Reward
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Loyalty_Value { get; set; }
        [Required]
        public int Discount { get; set; }


        //Navigation properties of child tables
        public virtual ICollection<Booking> bookings { get; set; }
        public virtual ICollection<Payment> payments { get; set; }


    }
}
