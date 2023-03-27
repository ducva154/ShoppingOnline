using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Product
{
    public class GetProductByCategoryResponse
    {
        public string Message { get; set; }
        public int NumberOfItems { get; set; }
        public IEnumerable<GetProductByCategoryItemResponse> Products { get; set; }
    }

    public class GetProductByCategoryItemResponse
    {
        public string Imange { get; set; }
        public string Name { get; set; }
        public string ProductId { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public bool IsAvailable { get; set; }
    }
}
