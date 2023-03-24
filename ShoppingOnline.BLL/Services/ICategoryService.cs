using ShoppingOnline.DTO.Models.Request.Category;
using ShoppingOnline.DTO.Models.Response.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services
{
    public interface ICategoryService
    {
        // Get All
        // Add
        AddCategoryResponse AddCategory(AddCategoryRequest request);
        // Update
        // Delete -  change status
    }
}
