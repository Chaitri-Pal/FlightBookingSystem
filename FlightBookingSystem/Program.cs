using FlightBookingSystem.BAL.Contacts;
using FlightBookingSystem.BAL.Services;
using FlightBookingSystem.DAL.Data;
using FlightBookingSystem.DAL.DataAccess;
using FlightBookingSystem.DAL.DataAccess.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



//Dependency injection uses abstraction we are accessing the interface and not showing how it is implemented

//adding db context
builder.Services.AddDbContext<ApplicationDBContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightDB"),b=>b.MigrationsAssembly("FlightBookingSystem"));
});
//We are passing interface and not create objects, if not then for each controller we have to do the steps separately

//JWT bearer configuration code
//used to add authentication service which is jwt bearer, this code will validate issuer, audience,token lifetime, validate issuer signing key
//would also pass the values to issuer audience and signing key defined in appsettings.json file
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

//Automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//inject business layer services 
builder.Services.AddScoped<IUserManager, UserManager>();
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

//make authentication service available to our appliation, should be called before authorisation
//because application first checks authentication, if valid then only authorization 
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
