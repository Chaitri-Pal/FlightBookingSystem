using FlightBookingSystem.DAL.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBookingSystem.DAL.Model;
using FlightBookingSystem.DAL.Data;
using Microsoft.EntityFrameworkCore;
using FlightBookingSystem.DAL.View_Model;

namespace FlightBookingSystem.DAL.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        //Whenever object of this class is created we'll call the respective repo slasses like customer, flight...
        // that will call the base class and the T in <T> will get replaced
        private readonly ApplicationDBContext _db;
        public UnitOfWork(ApplicationDBContext db) 
        {
            _db = db;
            User = new UserRepo(_db);
            Airport = new AirportRepo(_db);
            Booking = new BookingRepo(_db);
            Flight = new FlightRepo(_db);
            Payment = new PaymentRepo(_db);
            Reward = new RewardRepo(_db);
            Schedule = new ScheduleRepo(_db);

        }
        public IUserRepo User { get; private set; }
        public IAirportRepo Airport { get; private set; }
        public IBookingRepo Booking { get; private set; }
        public IFlightRepo Flight { get; private set; }
        public IPaymentRepo Payment { get; private set; }
        public IRewardRepo Reward { get; private set; }
        public IScheduleRepo Schedule { get; private set; }
        public void Save() 
        {
            //To make and save the changes in the actual database
            _db.SaveChangesAsync();
        }
    }

    //ICustomer interface given because in future if we need any class specific functions we can implement it
    public class UserRepo : Repo<User>, IUserRepo
    {
        private readonly ApplicationDBContext _db;
        public UserRepo(ApplicationDBContext db) :base(db) 
        {
            _db = db;
        }
    }
    public class FlightRepo : Repo<Flight>, IFlightRepo
    {
        private readonly ApplicationDBContext _db;
        public FlightRepo(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
    }
    public class AirportRepo : Repo<Airport>, IAirportRepo
    {
        private readonly ApplicationDBContext _db;
        public AirportRepo(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
    }
    public class BookingRepo : Repo<Booking>, IBookingRepo
    {
        private readonly ApplicationDBContext _db;
        public BookingRepo(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        
    }
    public class PaymentRepo : Repo<Payment>, IPaymentRepo
    {
        private readonly ApplicationDBContext _db;
        public PaymentRepo(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
    }
    public class RewardRepo : Repo<Reward>, IRewardRepo
    {
        private readonly ApplicationDBContext _db;
        public RewardRepo(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
    }
    public class ScheduleRepo : Repo<Schedule>, IScheduleRepo
    {
        private readonly ApplicationDBContext _db;
        public ScheduleRepo(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
    }
}
