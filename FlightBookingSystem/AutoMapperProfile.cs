using AutoMapper;
using FlightBookingSystem.DAL.Model;
using FlightBookingSystem.DAL.View_Model;

namespace FlightBookingSystem
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();

            CreateMap<AirportVM, Airport>();
            CreateMap<Airport, AirportVM>();

            CreateMap<Booking, BookingVM>();
            CreateMap<BookingVM, Booking>();

            CreateMap<Flight, FlightVM>();
            CreateMap<FlightVM, Flight>();

            CreateMap<Payment, PaymentVM>();
            CreateMap<PaymentVM, Payment>();

            CreateMap<Reward, RewardVM>();
            CreateMap<RewardVM, Reward>();

            CreateMap<Schedule, ScheduleVM>();
            CreateMap<ScheduleVM, Schedule>();
        }
    }
}
