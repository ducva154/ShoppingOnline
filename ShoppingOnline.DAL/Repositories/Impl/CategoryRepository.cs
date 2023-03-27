using Microsoft.EntityFrameworkCore;
using ShoppingOnline.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DAL.Repositories.Impl
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context) { }

        public IEnumerable<Category> GetListCategoriesByProduct(Product product)
        {
            var productEntry = _context.Entry(product);
            productEntry.Collection(product => product.Categories).Load();
            return product.Categories;
        }
    }
}
