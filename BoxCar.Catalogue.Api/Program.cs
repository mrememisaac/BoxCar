using BoxCar.Catalogue.Core.Contracts.Messaging;
using BoxCar.Catalogue.Messaging;
using BoxCar.Catalogue.Persistence;
using BoxCar.Integration.MessageBus;
using Microsoft.EntityFrameworkCore;
using BoxCar.Catalogue.Core.Extensions;
using BoxCar.Catalogue.Domain;
using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPersistenceServices(builder.Configuration);


//Specific DbContext for use from singleton AzServiceBusConsumer
var optionsBuilder = new DbContextOptionsBuilder<BoxCarDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(new VehicleRepository(optionsBuilder.Options));
builder.Services.AddSingleton(new EngineRepository(optionsBuilder.Options));
builder.Services.AddSingleton(new OptionPackRepository(optionsBuilder.Options));
builder.Services.AddSingleton(new ChassisRepository(optionsBuilder.Options));

builder.Services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();
builder.Services.AddSingleton<IChassisAzServiceBusConsumer, ChassisAddedEventConsumer>();
builder.Services.AddSingleton<IEngineAzServiceBusConsumer, EngineAddedEventConsumer>();
builder.Services.AddSingleton<IOptionPackAzServiceBusConsumer, OptionPackAddedEventConsumer>();
builder.Services.AddSingleton<IVehicleAzServiceBusConsumer, VehicleAddedEventConsumer>();

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

app.UseAzServiceBusConsumer();

app.Run();
