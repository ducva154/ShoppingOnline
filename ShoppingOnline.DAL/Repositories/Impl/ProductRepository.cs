using Microsoft.EntityFrameworkCore;
using ShoppingOnline.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DAL.Repositories.Impl
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context) { }

        public IEnumerable<Product> GetListProductByCategory(Category category)
        {
            var categoryEntry = _context.Entry(category);
            categoryEntry.Collection(category => category.Products).Load();
            return category.Products;
        }
    }
}
