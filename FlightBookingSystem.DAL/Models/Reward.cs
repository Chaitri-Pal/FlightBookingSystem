using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Models
{
    public class Reward
    {
        public Reward()
        {
            booking = new HashSet<Booking>();
        }
        [Key]
        [Required]
        public int Reward_Id { get; set; }
        [Required]
        public int loyalty_value { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public virtual ICollection<Booking> booking { get; set; }
        
    }
}
