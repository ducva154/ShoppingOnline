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
        GetAllCategoryResponse GetAllCategory();
        GetCategoryDetailRepsonse GetCategoryDetail(string categoryId);
        AddCategoryResponse AddCategory(AddCategoryRequest request);
        UpdateCategoryResponse UpdateCategory(string categoryId, UpdateCategoryRequest request);
        DeleteCategoryResponse DeleteCategory(string categoryId);
    }
}
