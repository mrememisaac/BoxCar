using System;
using System.Collections.ObjectModel;

namespace BoxCar.Ordering.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool OrderPaid { get; set; }

        public Collection<OrderLine> OrderLines { get; set; }
    }
}
