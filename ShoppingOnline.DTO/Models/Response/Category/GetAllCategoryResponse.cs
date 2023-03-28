using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Category
{
    public class GetAllCategoryResponse
    {
        public string Message { get; set; }
        public int NumberOfItems { get; set; }
        public IEnumerable<GetAllCategoryResponseItem> Categories { get; set; }
    }

    public class GetAllCategoryResponseItem
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
