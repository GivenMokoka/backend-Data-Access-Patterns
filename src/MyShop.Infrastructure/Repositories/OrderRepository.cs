using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class OrderRepository : GeneryRepository<Order>
    {
        public OrderRepository(ShoppingContext context) : base(context)
        {
        }
        /*You can override functionality in the repository to make sure that you include referenced entities and optionally eagerly load them*/
        public override IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            return context.Orders
                .Include(order => order.LineItems)
                .ThenInclude(LineItem => LineItem.Product)
                .Where(predicate)
                .ToList();
        }
        public override Order Update(Order entity)
        {
            var order = context.Orders
                .Include(o => o.LineItems)
                .ThenInclude(LineItem => LineItem.Product)
                .Single(s => s.OrderId == entity.OrderId);

            order.OrderDate = entity.OrderDate;
            order.LineItems = entity.LineItems;

            return base.Update(order);
        }
    }
}
