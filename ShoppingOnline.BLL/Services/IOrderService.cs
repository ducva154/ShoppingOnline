using ShoppingOnline.DTO.Models.Request.Order;
using ShoppingOnline.DTO.Models.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services
{
    public interface IOrderService
    {
        Task<AddOrderResponse> AddOrder(string userId, AddOrderRequest request);
        UpdateOrderResponse UpdateOrder(string orderId, UpdateOrderRequset request);
        Task<GetOrderByUserResponse> GetOrderByUser(string userId);
        GetOrderResponse GetOrderById(string orderId);
        GetAllOrderResponse GetAllOrder();
    }
}
