using BoxCar.Shared.Middlewares;
using BoxCar.ShoppingBasket;
using BoxCar.ShoppingBasket.DbContexts;
using BoxCar.ShoppingBasket.Repositories;
using BoxCar.ShoppingBasket.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

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
    c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:Catalogue:Uri"]))
    .AddPolicyHandler(CommunicationBreakdownPolicies.GetRetryPolicy())
                .AddPolicyHandler(CommunicationBreakdownPolicies.GetCircuitBreakerPolicy());
builder.Services.AddHttpClient<IChassisCatalogService, ChassisCatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:Catalogue:Uri"]))
    .AddPolicyHandler(CommunicationBreakdownPolicies.GetRetryPolicy())
                .AddPolicyHandler(CommunicationBreakdownPolicies.GetCircuitBreakerPolicy());
builder.Services.AddHttpClient<IEngineCatalogService, EngineCatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:Catalogue:Uri"]))
    .AddPolicyHandler(CommunicationBreakdownPolicies.GetRetryPolicy())
                .AddPolicyHandler(CommunicationBreakdownPolicies.GetCircuitBreakerPolicy());
builder.Services.AddHttpClient<IOptionPackCatalogService, OptionPackCatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:Catalogue:Uri"]))
    .AddPolicyHandler(CommunicationBreakdownPolicies.GetRetryPolicy())
                .AddPolicyHandler(CommunicationBreakdownPolicies.GetCircuitBreakerPolicy());

builder.Services.AddDbContext<ShoppingBasketDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
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
else
{
    app.UseMiddleware<GlobalErrorHandlerMiddleware>();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
