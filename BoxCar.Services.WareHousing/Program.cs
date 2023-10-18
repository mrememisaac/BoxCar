using BoxCar.Integration.MessageBus;
using BoxCar.Services.WareHousing.Contracts.Messaging;
using BoxCar.Services.WareHousing.DbContexts;
using BoxCar.Services.WareHousing.Extensions;
using BoxCar.Services.WareHousing.Messaging;
using BoxCar.Services.WareHousing.Repositories;
using BoxCar.Services.WareHousing.Worker;
using BoxCar.Shared.Logging;
using BoxCar.Shared.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Logging.ConfigureLogger);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ItemsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IItemsRepository, ItemsRepository>();

//Specific DbContext for use from singleton AzServiceBusConsumer
var optionsBuilder = new DbContextOptionsBuilder<ItemsDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(new ItemsRepository(optionsBuilder.Options));

builder.Services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();
builder.Services.AddSingleton<IChassisAzServiceBusConsumer, ChassisAddedEventConsumer>();
builder.Services.AddSingleton<IEngineAzServiceBusConsumer, EngineAddedEventConsumer>();
builder.Services.AddSingleton<IVehicleAzServiceBusConsumer, VehicleAddedEventConsumer>();
builder.Services.AddSingleton<IOptionPackAzServiceBusConsumer, OptionPackAddedEventConsumer>();
builder.Services.AddHostedService<OrderFulfillmentService>();

builder.Services.AddSwaggerGen();

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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAzServiceBusConsumer();

app.Run();
