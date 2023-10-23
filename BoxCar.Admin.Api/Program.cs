using BoxCar.Shared.Middlewares;
using BoxCar.Admin.Core;
using BoxCar.Admin.Persistence;
using BoxCar.Integration.MessageBus;
using BoxCar.Shared.Logging;
using Serilog;
using BoxCar.Admin.Core.Contracts.Identity;
using Microsoft.EntityFrameworkCore;
using BoxCar.Admin.Api.Identity;
using BoxCar.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Logging.ConfigureLogger);
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var optionsBuilder = new DbContextOptionsBuilder<BoxCarAdminDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});
builder.Services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();


builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration["RedisConnectionString"];
    //option.ConfigurationOptions.AbortOnConnectFail = false;
});

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddHealthChecks().AddDbContextCheck<BoxCarAdminDbContext>();

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

app.UseSerilogRequestLogging();
app.Run();
