using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy.Proxies;
using MyShop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class CustomerRepository : GeneryRepository<Customer>
    {
        public CustomerRepository(ShoppingContext context) : base(context)
        {
        }
        //todo: Override the rest of the methods for retrieval in the repository to apply the value holder.
        public override IEnumerable<Customer> All()
        {
            return base.All().Select(MapToProxy);

        }
        public override Customer Update(Customer entity)
        {
            var customer = context.Customers
               .Single(c => c.CustomerId == entity.CustomerId);

            customer.Name = entity.Name;
            customer.City = entity.City;
            customer.PostalCode = entity.PostalCode;
            customer.ShippingAddress = entity.ShippingAddress;
            customer.Country = entity.Country;

            return base.Update(customer);
        }

        private CustomerProxy MapToProxy(Customer customer)
        {
            return new CustomerProxy
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                ShippingAddress = customer.ShippingAddress,
                City = customer.City,
                PostalCode = customer.PostalCode,
                Country = customer.Country
            };
        }
    }
}
