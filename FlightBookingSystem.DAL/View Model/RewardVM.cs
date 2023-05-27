using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.View_Model
{
    public class RewardVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Loyalty_Value { get; set; }
        [Required]
        public int Discount { get; set; }
    }
}
