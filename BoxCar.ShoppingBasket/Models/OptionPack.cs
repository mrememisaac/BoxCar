namespace BoxCar.ShoppingBasket.Models
{
    public class OptionPack
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;


        public List<Option> Options = new List<Option>();

    }
}
