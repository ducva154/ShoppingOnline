using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Category
{
    public class GetCategoryDetailRepsonse
    {
        public string Message { get; set; }
        public CategoryDetailResponse Category { get; set; }
    }

    public class CategoryDetailResponse
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
    }
}
