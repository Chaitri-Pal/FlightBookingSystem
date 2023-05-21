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
        [ForeignKey("Booking_Id")]
        public virtual Booking booking { get; set; }        
        [Required]
        public string P_type { get; set; }
        [Required]
        public bool P_status { get; set; }
        [Required]
        public DateTime Payment_date { get; set; }
        [Required]
        public double Amount { get; set; }

    }
}
