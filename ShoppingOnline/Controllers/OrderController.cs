using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.BLL.Services;
using ShoppingOnline.BLL.Services.Impl;
using ShoppingOnline.DTO.Models.Request.Order;

namespace ShoppingOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Add/{userId}")]
        public async Task<IActionResult> AddOrder(string userId, [FromBody] AddOrderRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderService.AddOrder(userId, request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpPut("Update/{orderId}")]
        public IActionResult UpdateOrder(string orderId, [FromBody] UpdateOrderRequset request)
        {
            if (ModelState.IsValid)
            {
                var response = _orderService.UpdateOrder(orderId, request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpGet("GetByUser/{userId}")]
        public async Task<IActionResult> GetOrderByUser(string userId)
        {
            var response = await _orderService.GetOrderByUser(userId);
            return Ok(response);
        }

        [HttpGet("GetById/{orderId}")]
        public IActionResult GetOrderById(string orderId)
        {
            var response = _orderService.GetOrderById(orderId);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllOrder()
        {
            var response = _orderService.GetAllOrder();
            return Ok(response);
        }
    }
}
