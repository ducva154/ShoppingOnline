using ShoppingOnline.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Request.Order
{
    public class AddOrderRequest
    {
        public IEnumerable<string> CartIds { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public double Amount { get; set; }
    }
}
