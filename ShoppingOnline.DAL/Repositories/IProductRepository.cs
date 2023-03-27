using ShoppingOnline.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DAL.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IEnumerable<Product> GetListProductByCategory(Category category);
    }
}
