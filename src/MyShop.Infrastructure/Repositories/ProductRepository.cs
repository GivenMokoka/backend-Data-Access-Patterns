using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class ProductRepository : GeneryRepository<Product>
    {
        public ProductRepository(ShoppingContext context) : base(context)
        {
        }
        public override Product Update(Product entity)
        {
            var product = context.Products
               .Single(p => p.ProductId == entity.ProductId);

            product.Price = entity.Price;
            product.Name = entity.Name;

            return base.Update(product);
        }
    }
}
