using FlightBookingSystem.DAL.Model;
using FlightBookingSystem.DAL.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.DAL.DataAccess.Interface
{
    public interface IUnitOfWork
    {
        //interface cannot set
        ICustomerRepo Customer { get; }
        IAirportRepo Airport { get; }
        IBookingRepo Booking { get; }
        IFlightRepo Flight { get; }
        IPaymentRepo Payment { get; }
        IRewardRepo Reward { get; }
        IScheduleRepo Schedule { get; }
        void Save();
    }
    public interface ICustomerRepo : IRepo<Customer> { }
    public interface IAirportRepo : IRepo<Airport> { }

    

    public interface IBookingRepo : IRepo<Booking> { }
    public interface IFlightRepo : IRepo<Flight> { }
    public interface IPaymentRepo : IRepo<Payment> { }
    public interface IRewardRepo : IRepo<Reward> { }
    public interface IScheduleRepo : IRepo<Schedule> { }

}
