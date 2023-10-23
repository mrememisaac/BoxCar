using BoxCar.Integration.MessageBus;
using BoxCar.Shared.Logging;
using BoxCar.Shared.Middlewares;
using BoxCar.ShoppingBasket;
using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Extensions;
using BoxCar.ShoppingBasket.Messaging;
using BoxCar.ShoppingBasket.Repositories;
using BoxCar.ShoppingBasket.Repositories.Contracts;
using BoxCar.ShoppingBasket.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Logging.ConfigureLogger);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketLinesRepository, BasketLinesRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IEngineRepository, EngineRepository>();
builder.Services.AddScoped<IOptionPackRepository, OptionPackRepository>();
builder.Services.AddScoped<IChassisRepository, ChassisRepository>();
builder.Services.AddHttpClient<IVehicleCatalogService, VehicleCatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:CatalogueService:Uri"]))
    .AddPolicyHandler(CommunicationBreakdownPolicies.GetRetryPolicy())
                .AddPolicyHandler(CommunicationBreakdownPolicies.GetCircuitBreakerPolicy());
builder.Services.AddHttpClient<IChassisCatalogService, ChassisCatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:CatalogueService:Uri"]))
    .AddPolicyHandler(CommunicationBreakdownPolicies.GetRetryPolicy())
                .AddPolicyHandler(CommunicationBreakdownPolicies.GetCircuitBreakerPolicy());
builder.Services.AddHttpClient<IEngineCatalogService, EngineCatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:CatalogueService:Uri"]))
    .AddPolicyHandler(CommunicationBreakdownPolicies.GetRetryPolicy())
                .AddPolicyHandler(CommunicationBreakdownPolicies.GetCircuitBreakerPolicy());
builder.Services.AddHttpClient<IOptionPackCatalogService, OptionPackCatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:CatalogueService:Uri"]))
    .AddPolicyHandler(CommunicationBreakdownPolicies.GetRetryPolicy())
                .AddPolicyHandler(CommunicationBreakdownPolicies.GetCircuitBreakerPolicy());


builder.Services.AddDbContext<ShoppingBasketDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Specific DbContext for use from singleton AzServiceBusConsumer
var optionsBuilder = new DbContextOptionsBuilder<ShoppingBasketDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(sp => new BoxCar.ShoppingBasket.Repositories.Consumers.VehicleRepository(optionsBuilder.Options));
builder.Services.AddSingleton(sp => new BoxCar.ShoppingBasket.Repositories.Consumers.EngineRepository(optionsBuilder.Options));
builder.Services.AddSingleton(sp => new BoxCar.ShoppingBasket.Repositories.Consumers.OptionPackRepository(optionsBuilder.Options));
builder.Services.AddSingleton(sp => new BoxCar.ShoppingBasket.Repositories.Consumers.ChassisRepository(optionsBuilder.Options));

builder.Services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();
builder.Services.AddSingleton<IChassisAzServiceBusConsumer, ChassisAddedEventConsumer>();
builder.Services.AddSingleton<IEngineAzServiceBusConsumer, EngineAddedEventConsumer>();
builder.Services.AddSingleton<IOptionPackAzServiceBusConsumer, OptionPackAddedEventConsumer>();
builder.Services.AddSingleton<IVehicleAzServiceBusConsumer, VehicleAddedEventConsumer>();

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddHealthChecks().AddDbContextCheck<ShoppingBasketDbContext>();

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
