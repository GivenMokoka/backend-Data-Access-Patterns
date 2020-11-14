using MyShop.Domain.Lazy;
using MyShop.Domain.Models;
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
            return base.All().Select(s =>
            {
                s.profilePictureValueHolder = new ValueHolder<byte[]>((parameter) =>
                {
                    return ProfilePictureService.GetFor(parameter.ToString());
                });
                return s;
            });

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
    }
}
