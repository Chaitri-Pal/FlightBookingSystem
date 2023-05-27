using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.View_Model
{
    public class BookingVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Booking_date { get; set; }
        [Required]
        public string B_status { get; set; }

        //Foreign keys
        public int Schedule_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Reward_Id { get; set; }
    }
}
