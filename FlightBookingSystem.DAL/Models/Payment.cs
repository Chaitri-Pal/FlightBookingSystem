using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Models
{
    public class Payment
    {
        [Key]
        [Required]
        public int Payment_Id { get; set; }
        [Required]
        public int Booking_Id { get; set; }
        [Required]
        public int Cust_ID { get; set; }
        [Required]
        public string P_type { get; set; }
        [Required]
        public bool P_status { get; set; }
        [Required]
        public DateTime Payment_date { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int Reward_Id { get; set; }
        [ForeignKey("Booking_Id")]
        [Required]
        public virtual Booking booking { get; set; }
        [ForeignKey("Customer_Id")]
        
        [Required]
        public virtual Customer customer { get; set; }
        [ForeignKey("Reward_Id")]
        [Required]
        public virtual Reward reward { get; set; }
    }
}
