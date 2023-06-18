using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Model
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string P_Type { get; set; }
        [Required]
        public string P_Status { get; set; }
        [Required]
        public DateTime Payment_date { get; set; }
        [Required]
        public int Amount { get; set; }


        //Foreign Keys
        [Required]
        public int User_Id { get; set; }
        [Required]
        public int Booking_Id { get; set; }
        [Required]
        public int Reward_Id { get; set; }



        //Navigation properties of child/parent  tables
        public virtual User users { get; set; }
        public virtual Booking bookings { get; set; }
        public virtual Reward rewards { get; set; }
        


    }
}
