using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Models
{
    public class Booking
    {
        [Key]
        [Required]
        public int Booking_Id { get; set; }
        [Required]

        public int Schedule_Id { get; set; }
        [Required]

        public int Cust_ID { get; set; }
        [Required]
        public DateTime Booking_date { get; set; }
        [Required]
        public bool B_status { get; set; }
        [Required]
        public int Reward_Id { get; set; }
        [ForeignKey("Schedule_Id")]
        public virtual Schedule schedule { get; set; }
        [ForeignKey("Customer_Id")]
        public virtual Customer customer { get; set; }
        [ForeignKey("Reward_Id")]

        public virtual Reward reward { get; set; }
        public virtual ICollection<Payment> payment { get; set; }
    }
}
