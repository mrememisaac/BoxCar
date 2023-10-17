using BoxCar.Integration.MessageBus;
using BoxCar.Services.Payment.Services;
using BoxCar.Services.Payment.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IExternalGatewayPaymentService, ExternalGatewayPaymentService>(c =>
    c.BaseAddress = new Uri(builder.Configuration["ApiConfigs:ExternalPaymentGateway:Uri"]));
builder.Services.AddHostedService<PaymentRequestService>();
builder.Services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();

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
