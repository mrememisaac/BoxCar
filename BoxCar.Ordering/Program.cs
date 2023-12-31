using BoxCar.Integration.MessageBus;
using BoxCar.Ordering.DbContexts;
using BoxCar.Ordering.Extensions;
using BoxCar.Ordering.Messaging;
using BoxCar.Ordering.Repositories;
using BoxCar.Shared.Logging;
using BoxCar.Shared.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Logging.ConfigureLogger);
// Add builder.Services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//Specific DbContext for use from singleton AzServiceBusConsumer
var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(new OrderRepository(optionsBuilder.Options));

builder.Services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();
builder.Services.AddSingleton<IAzServiceBusConsumer, CheckoutMessageServiceBusConsumer>();

builder.Services.AddSwaggerGen();

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddHealthChecks().AddDbContextCheck<OrderDbContext>();

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
app.UseSerilogRequestLogging();

app.Run();
