using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.View_Model
{
    public class PaymentVM
    {
        [Required]
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
        public int Customer_Id { get; set; }
        public int Booking_Id { get; set; }
        public int Reward_Id { get; set; }
    }
}
