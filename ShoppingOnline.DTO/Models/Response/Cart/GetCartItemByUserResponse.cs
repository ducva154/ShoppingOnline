using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Cart
{
    public class GetCartItemByUserResponse
    {
        public string Message { get; set; }
        public int NumberOfItems { get; set; }
        public IEnumerable<GetCartItemByUserResponseItem> CartItems { get; set; }
    }

    public class GetCartItemByUserResponseItem
    {
        public string CartItemId { get; set; }
        public string ProductId { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }

    }
}
