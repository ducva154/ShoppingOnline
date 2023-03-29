using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Order
{
    public class GetAllOrderResponse
    {
        public string Message { get; set; }
        public int NumberOfItem { get; set; }
        public IEnumerable<GetAllOrderItem> Orders { get; set; }
    }

    public class GetAllOrderItem
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public double Amount { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public DateTime Date { get; set; }
        public bool Paided { get; set; }
        public string Status { get; set; }
    }
}
