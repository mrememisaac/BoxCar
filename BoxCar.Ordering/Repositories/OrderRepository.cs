using BoxCar.Ordering.DbContexts;
using BoxCar.Ordering.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxCar.Ordering.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<OrderDbContext> dbContextOptions;

        public OrderRepository(DbContextOptions<OrderDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task<List<Order>> GetOrdersForUser(Guid userId)
        {
            await using var _orderDbContext = new OrderDbContext(dbContextOptions);
            return await _orderDbContext.Orders
                .Include(o => o.OrderLines)
                .Where(o => o.UserId == userId).OrderBy(o => o.OrderPlaced).ToListAsync();
        }

        public async Task AddOrder(Order order)
        {
            await using (var _orderDbContext = new OrderDbContext(dbContextOptions))
            {
                await _orderDbContext.Orders.AddAsync(order);
                await _orderDbContext.SaveChangesAsync();
            }
        }

        public async Task<Order> GetOrderById(Guid orderId)
        {
            using (var _orderDbContext = new OrderDbContext(dbContextOptions))
            {
                return await _orderDbContext.Orders
                            .Include(o => o.OrderLines)
                            .Where(o => o.Id == orderId).FirstOrDefaultAsync();
            }
        }

        public async Task UpdateOrderPaymentStatus(Guid orderId, bool paid)
        {
            using (var _orderDbContext = new OrderDbContext(dbContextOptions))
            {
                var order = await _orderDbContext.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync();
                order.OrderPaid = paid;
                order.FulfillmentStatus = FulfillmentStatus.Approved;
                await _orderDbContext.SaveChangesAsync();
            }
        }

        public async Task CancelOrder(Order orderToCancel)
        {
            using (var _orderDbContext = new OrderDbContext(dbContextOptions))
            {
                var order = await _orderDbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderToCancel.Id);
                order.FulfillmentStatus = FulfillmentStatus.Cancelled;
                await _orderDbContext.SaveChangesAsync();
            }
        }
    }
}
