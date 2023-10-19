using BoxCar.Catalogue.Core.Contracts.Messaging;
using BoxCar.Catalogue.Messaging;
using BoxCar.Catalogue.Persistence;
using BoxCar.Integration.MessageBus;
using Microsoft.EntityFrameworkCore;
using BoxCar.Catalogue.Core.Extensions;
using BoxCar.Catalogue.Domain;
using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Persistence.Repositories;
using BoxCar.Catalogue.Core;
using BoxCar.Shared.Middlewares;
using BoxCar.Shared.Logging;
using Serilog;
using BoxCar.Catalogue.Api.Identity;
using BoxCar.Catalogue.Core.Contracts.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Logging.ConfigureLogger);
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);


//Specific DbContext for use from singleton AzServiceBusConsumer
var optionsBuilder = new DbContextOptionsBuilder<BoxCarCatalogueDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(sp => new VehicleRepository(optionsBuilder.Options));
builder.Services.AddSingleton(sp => new EngineRepository(optionsBuilder.Options));
builder.Services.AddSingleton(sp => new OptionPackRepository(optionsBuilder.Options));
builder.Services.AddSingleton(sp => new ChassisRepository(optionsBuilder.Options));

builder.Services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();
builder.Services.AddSingleton<IChassisAzServiceBusConsumer, ChassisAddedEventConsumer>();
builder.Services.AddSingleton<IEngineAzServiceBusConsumer, EngineAddedEventConsumer>();
builder.Services.AddSingleton<IOptionPackAzServiceBusConsumer, OptionPackAddedEventConsumer>();
builder.Services.AddSingleton<IVehicleAzServiceBusConsumer, VehicleAddedEventConsumer>();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMiddleware<GlobalErrorHandlerMiddleware>();
}
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAzServiceBusConsumer();

app.Run();
