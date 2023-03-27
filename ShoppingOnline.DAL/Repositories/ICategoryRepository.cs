using ShoppingOnline.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DAL.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        IEnumerable<Category> GetListCategoriesByProduct(Product product);
    }
}
