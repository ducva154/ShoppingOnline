using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Product
{
    public class GetAllProductResponse
    {
        public string Message { get; set; }
        public int NumberOfItems { get; set; }
        public IEnumerable<GetAllProductItemResponse> Products { get; set; }
    }

    public class GetAllProductItemResponse
    {
        public string Image { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public int StockQuantity { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
    }
}
