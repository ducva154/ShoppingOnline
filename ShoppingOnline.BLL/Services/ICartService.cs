using ShoppingOnline.DTO.Models.Request.Cart;
using ShoppingOnline.DTO.Models.Response.Cart;
using ShoppingOnline.DTO.Models.Response.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services
{
    public interface ICartService
    {
        Task<AddCartItemResponse> AddToCart(string userId, AddCartItemRequest request);
        RemoveCartItemResponse RemoveFromCart(string itemId);
        UpdateCartItemResponse UpdateCartItem(string itemId, UpdateCartItemRequest request);
        Task<GetCartItemByUserResponse> GetCartItemByUser(string userId);
    }
}
