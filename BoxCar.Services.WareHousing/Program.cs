using BoxCar.Integration.MessageBus;
using BoxCar.Services.WareHousing.DbContexts;
using BoxCar.Services.WareHousing.Messaging;
using BoxCar.Services.WareHousing.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSingleton<IAzServiceBusConsumer, ChassisAddedEventConsumer>();
builder.Services.AddSingleton<IAzServiceBusConsumer, EngineAddedEventConsumer>();
builder.Services.AddSingleton<IAzServiceBusConsumer, VehicleAddedEventConsumer>();
builder.Services.AddSingleton<IAzServiceBusConsumer, OptionPackAddedEventConsumer>();


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
