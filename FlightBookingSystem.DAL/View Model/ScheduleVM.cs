using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.View_Model
{
    public class ScheduleVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Dep_Time { get; set; }
        [Required]
        public DateTime Arr_Time { get; set; }


        //Foreign Keys
        public int Dep_id { get; set; }
        public int Arr_id { get; set; }
        public int Flight_Id { get; set; }
    }
}
