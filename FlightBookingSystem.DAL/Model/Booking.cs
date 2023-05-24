﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.Model
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Booking_date { get; set; }
        [Required]
        public string B_status { get; set; }

        //Foreign keys
        public int Schedule_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Reward_Id { get; set; }


        //Navigation properties of child/parent  tables
        public virtual Schedule schedules { get; set; }
        public virtual Customer customers { get; set; }
        public virtual Reward rewards { get; set; }
        public virtual ICollection<Payment> payments { get; set; }
    }
}
