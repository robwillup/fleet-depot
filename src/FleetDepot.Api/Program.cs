using FleetDepot.Types.Dtos;
using FleetDepot.Core.Mappers;
using FleetDepot.Core.Services;
using FleetDepot.Dal;
using FleetDepot.Types.Models;
using FleetDepot.Dal.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
if (string.IsNullOrEmpty(urls))
{
    builder.WebHost.UseUrls("http://0.0.0.0:5084");
}

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FleetDepotDb>(options =>
{
    System.Console.WriteLine(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
    if (builder.Environment.IsProduction() || builder.Environment.IsDevelopment())
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    else
    {
        options.UseInMemoryDatabase(builder.Configuration.GetValue<string>("dbName")!);
    }
});
builder.Services.AddScoped<IVehicleRepository<Vehicle>, VehicleRepository<Vehicle>>();
builder.Services.AddScoped<IVehicleService<VehicleDto>, VehicleService<VehicleDto, Vehicle>>();
builder.Services.AddScoped<IVehicleService<CarDto>, CarService>();
builder.Services.AddScoped<IVehicleService<BusDto>, BusService>();
builder.Services.AddScoped<IVehicleService<TruckDto>, TruckService>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFleetDepot.Ui", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

System.Console.WriteLine(app.Configuration.GetValue<string>("dbName"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FleetDepot API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowFleetDepot.Ui");

app.MapControllers();
//app.UseHttpsRedirection();

app.Run();
