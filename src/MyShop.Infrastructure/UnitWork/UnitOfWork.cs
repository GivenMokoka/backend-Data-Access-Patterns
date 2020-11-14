using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.UnitWork
{
    /*
     * we are only leveraging the unit of word pattern indide our order controller, because 
     * if we take a look at the customer controller, this controller don´t leverage to different 
     * types of entities or repositories. So there´s no  need for us to inject the unit of work into 
     * this class here because one of the problems with the unit of work is that from an outside perspective,
     * we might not know which of the repositories in our unit of work that our controller or action or 
     * method that we´re calling is leveraging in  a really large solution. You might break  it up and 
     * have multiple different, event if works with only the repositories that makes sence to be changed 
     * together. Because if you only have one large unit of work and you´re passing that into a controller, 
     * you really have no clue what that particular controller or action or method is updating
     */
    public class UnitOfWork : IUnitOfWork
    {
        private ShoppingContext context;

        public UnitOfWork(ShoppingContext context)
        {
            this.context = context;
        }
        private readonly IRepository<Customer> customerRepository;
        public IRepository<Customer> CustomerRepository {
            get
            {
                //lazy initialization
                return customerRepository ?? new CustomerRepository(context);
            }
        }

        private readonly IRepository<Order> orderRepository;
        public IRepository<Order> OrderRepository
        {
            get
            {
                //lazy initialization
                return orderRepository ?? new OrderRepository(context);
            }
        }

        private IRepository<Product> productRepository;
        public IRepository<Product> ProductRepository
        {
            get
            {
                //lazy initialization
                return productRepository ?? new ProductRepository(context);
            }
        }        

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
