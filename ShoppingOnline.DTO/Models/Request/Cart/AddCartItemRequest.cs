using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Request.Cart
{
    public class AddCartItemRequest
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
