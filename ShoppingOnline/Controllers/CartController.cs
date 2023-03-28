using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.BLL.Services;
using ShoppingOnline.BLL.Services.Impl;
using ShoppingOnline.DTO.Models.Request.Cart;

namespace ShoppingOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("Add/{userId}")]
        public async Task<IActionResult> AddCartItem(string userId, [FromBody] AddCartItemRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _cartService.AddToCart(userId, request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpPut("Update/{itemId}")]
        public IActionResult UpdateCartItem(string itemId, [FromBody] UpdateCartItemRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = _cartService.UpdateCartItem(itemId, request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpDelete("Delete/{itemId}")]
        public IActionResult DeleteCartItem(string itemId)
        {
            var response = _cartService.RemoveFromCart(itemId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetByUser/{userId}")]
        public async Task<IActionResult> GetCartItemByUser(string userId)
        {
            var response = await _cartService.GetCartItemByUser(userId);
            return Ok(response);
        }
    }
}
