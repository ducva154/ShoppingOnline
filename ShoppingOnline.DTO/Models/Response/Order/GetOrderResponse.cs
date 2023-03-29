using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Order
{
    public class GetOrderResponse
    {
        public string Message { get; set; }
        public GetOrderItem Order { get; set; }
    }

    public class GetOrderItem
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Status { get; set; }
        public bool Paided { get; set; }
        public IEnumerable<GetOrderDetailItem> OrderDetails { get; set; }
    }

    public class GetOrderDetailItem
    {
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}
