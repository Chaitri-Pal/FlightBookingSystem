using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Models
{
    public class Customer
    {
        public Customer()
        {
            bookings = new HashSet<Booking>();
            payments = new HashSet<Payment>();
        }
        [Key]
        [Required]
        public int Customer_Id { get; set; }
        [Required]
        public string C_Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Aadhar { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        
        public virtual ICollection<Booking> bookings { get; set; }
        
        public virtual ICollection<Payment> payments { get; set; }


    }
}
