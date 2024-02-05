using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;


namespace Ordering.Infrastructure.Repositories
{
    internal class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {

        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            var Order = await _context.orders.Where(x=>x.UserName == userName).ToListAsync();
            return Order;
        }
    }
}
