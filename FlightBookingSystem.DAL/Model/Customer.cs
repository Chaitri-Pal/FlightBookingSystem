using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string C_Name { get; set;}
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


        //Navigation properties of child tables
        public virtual ICollection<Booking> bookings { get; set; }
        public virtual ICollection<Payment> payments { get; set; }


    }
}
