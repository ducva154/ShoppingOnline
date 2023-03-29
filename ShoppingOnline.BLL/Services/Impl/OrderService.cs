using Microsoft.AspNetCore.Identity;
using ShoppingOnline.DAL.Repositories;
using ShoppingOnline.DTO.Constants;
using ShoppingOnline.DTO.Entities;
using ShoppingOnline.DTO.Models.Request.Order;
using ShoppingOnline.DTO.Models.Response.Cart;
using ShoppingOnline.DTO.Models.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<CustomUser> _userManager;

        public OrderService(IUnitOfWork unitOfWork, UserManager<CustomUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<AddOrderResponse> AddOrder(string userId, AddOrderRequest request)
        {
            if (request == null)
            {
                return new AddOrderResponse
                {
                    Message = "Add order request is null!",
                    IsSuccess = false
                };
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AddOrderResponse
                {
                    Message = "User not found!",
                    IsSuccess = false
                };
            }

            bool isPaid = false;
            // Xu ly thanh toan - lam sau

            try
            {
                var order = new Order
                {
                    //Amount = request.Amount,
                    Address = request.Address,
                    Contact = request.Contact,
                    Paided = isPaid,
                    Date = DateTime.Now,
                    Status = OrderStatusConstant.PENDING,
                    UserId = userId,
                };
                _unitOfWork.OrderRepository.Add(order);
                _unitOfWork.SaveChanges();

                double amount = 0;
                request.CartIds.ToList().ForEach(x =>
                {
                    var cartItem = _unitOfWork.CartItemRepository.GetId(Guid.Parse(x));
                    var product = cartItem.Product;
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = product.ProductId,
                        Price = product.Price,
                        Quantity = cartItem.Quantity,
                    };
                    _unitOfWork.OrderDetailRepository.Add(orderDetail);
                    _unitOfWork.CartItemRepository.Remove(cartItem);
                    amount = amount + orderDetail.Total;

                    product.StockQuantity = product.StockQuantity - orderDetail.Quantity;
                    _unitOfWork.ProductRepository.Update(product);
                });
                order.Amount = amount;
                _unitOfWork.OrderRepository.Update(order);
                _unitOfWork.SaveChanges();

                return new AddOrderResponse
                {
                    Message = "Add order succesfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new AddOrderResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<GetOrderByUserResponse> GetOrderByUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new GetOrderByUserResponse
                {
                    Message = "User not found!",
                    NumberOfItems = 0
                };
            }

            var orders = user.Orders;
            if (orders.Count() == 0)
            {
                return new GetOrderByUserResponse
                {
                    Message = "Mo item!",
                    NumberOfItems = 0
                };
            }

            return new GetOrderByUserResponse
            {
                Message = "Success",
                NumberOfItems = orders.Count(),
                Orders = orders.Select(order => new GetOrderByUserItem
                {
                    OrderId = order.OrderId.ToString(),
                    Date = order.Date,
                    Total = order.Amount,
                    Status = order.Status
                })
            };
        }

        public GetOrderResponse GetOrderById(string orderId)
        {
            var order = _unitOfWork.OrderRepository.GetId(Guid.Parse(orderId));
            if (order == null)
            {
                return new GetOrderResponse
                {
                    Message = "Order not found!"
                };
            }

            var user = order.User;
            if (user == null)
            {
                return new GetOrderResponse
                {
                    Message = "User not found!"
                };
            }

            var orderDetails = order.OrderDetails;

            var orderItem = new GetOrderItem
            {
                OrderId = orderId,
                Date = order.Date,
                UserFullName = user.FullName,
                UserEmail = user.Email,
                UserPhoneNumber = user.PhoneNumber,
                Status = order.Status,
                Paided = order.Paided,
                Address = order.Address,
                Contact = order.Contact,
                OrderDetails = orderDetails.Select(orderDetail => new GetOrderDetailItem
                {
                    ProductId = orderDetail.ProductId.ToString(),
                    ProductImage = orderDetail.Product.Image,
                    ProductName = orderDetail.Product.Name,
                    Price = orderDetail.Price,
                    Quantity = orderDetail.Quantity,
                    Total = orderDetail.Total
                })
            };

            return new GetOrderResponse
            {
                Message = "Success",
                Order = orderItem
            };
        }

        public GetAllOrderResponse GetAllOrder()
        {
            var orders = _unitOfWork.OrderRepository.GetAll();
            if (orders.Count() == 0)
            {
                return new GetAllOrderResponse
                {
                    Message = "No item!",
                    NumberOfItem = 0
                };
            }

            return new GetAllOrderResponse
            {
                Message = "Success",
                NumberOfItem = orders.Count(),
                Orders = orders.Select(order => new GetAllOrderItem
                {
                    OrderId = order.OrderId.ToString(),
                    UserId = order.UserId,
                    Amount = order.Amount,
                    Address = order.Address,
                    Contact = order.Contact,
                    Date = order.Date,
                    Paided = order.Paided,
                    Status = order.Status
                })
            };
        }

        public UpdateOrderResponse UpdateOrder(string orderId, UpdateOrderRequset request)
        {
            var order = _unitOfWork.OrderRepository.GetId(Guid.Parse(orderId));
            if (order == null)
            {
                return new UpdateOrderResponse
                {
                    Message = "Order not found!",
                    IsSuccess = false,
                };
            }

            try
            {
                order.Status = request.Status;
                _unitOfWork.OrderRepository.Update(order);
                _unitOfWork.SaveChanges();
                return new UpdateOrderResponse
                {
                    Message = "Update order successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new UpdateOrderResponse
                {
                    Message = "Can't update order!",
                    IsSuccess = false,
                    Error = ex.Message  
                };
            }
        }
    }
}
