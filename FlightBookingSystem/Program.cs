using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.BAL.Services;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess;
using FlightBookingSystem.DAL.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



//Dependency injection uses abstraction we are accessing the interface and not showing how it is implemented

//adding db context
builder.Services.AddDbContext<ApplicationDBContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightDB"),b=>b.MigrationsAssembly("FlightBookingSystem"));
});
//We are passing interface and not create objects, if not then for each controller we have to do the steps separately



//inject business layer services 
builder.Services.AddScoped<ICustomerManager, CustomerManager>();
builder.Services.AddScoped<IAirportManager, AirportManager>();
builder.Services.AddScoped<IBookingManager, BookingManager>();
builder.Services.AddScoped<IFlightManager, FlightManager>();
builder.Services.AddScoped<IPaymentManager, PaymentManager>();
builder.Services.AddScoped<IRewardManager, RewardManager>();
builder.Services.AddScoped<IScheduleManager, ScheduleManager>();

//Inject Data Dependency Data Access to the program
//Using AddScoped as it uses only 1 instance. Whereas AddTransient where it creates 2 instances which may lead to conflict of resourses to avoid that use scoped that has one instance throughout.
//to get value from database scope maybe used to save save data use scoped
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
