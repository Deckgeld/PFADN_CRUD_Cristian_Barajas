using ATOS.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Passengers.ApplicationServices.Passengers;
using Passengers.Core.Passengers;
using Passengers.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = builder.Configuration.GetConnectionString("Default");
var connectionString = "server=localhost;port=3306;database=crudpassengers;user=root;password=Conalepmorelia2;CharSet=utf8;SslMode=none;Pooling=false;AllowPublicKeyRetrieval=True;";
builder.Services.AddDbContext<PassengersDataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddAutoMapper(typeof(Passengers.ApplicationServices.MapperProfile));

builder.Services.AddTransient<IRepository<int, Passenger>, Repository<int, Passenger>>();
builder.Services.AddTransient<IPassengersAppService, PassengersAppService>();

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
