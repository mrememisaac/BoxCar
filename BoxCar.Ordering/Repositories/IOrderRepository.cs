using BoxCar.Ordering.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxCar.Ordering.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersForUser(Guid userId);
        Task AddOrder(Order order);
        Task<Order> GetOrderById(Guid orderId);
        Task UpdateOrderPaymentStatus(Guid orderId, bool paid);

    }
}