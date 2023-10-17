namespace BoxCar.Services.Payment.Models
{
    public class EmailInfo
    {
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }
        
        public string Email { get; set; }
        
        public string Message { get; set; }
    }
}
