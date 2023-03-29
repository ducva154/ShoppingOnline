using Microsoft.AspNetCore.Identity;
using ShoppingOnline.DAL.Repositories;
using ShoppingOnline.DTO.Entities;
using ShoppingOnline.DTO.Models.Request.Cart;
using ShoppingOnline.DTO.Models.Response.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<CustomUser> _userManager;

        public CartService(IUnitOfWork unitOfWork, UserManager<CustomUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<AddCartItemResponse> AddToCart(string userId, AddCartItemRequest request)
        {
            if (request == null)
            {
                return new AddCartItemResponse
                {
                    Message = "Add to cart request is null!",
                    IsSuccess = false
                };
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AddCartItemResponse
                {
                    Message = "User not found!",
                    IsSuccess = false
                };
            }

            var product = _unitOfWork.ProductRepository.GetId(Guid.Parse(request.ProductId));
            if (product == null)
            {
                return new AddCartItemResponse
                {
                    Message = "Product not found!",
                    IsSuccess = false
                };
            }

            try
            {
                _unitOfWork.CartItemRepository.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    Quantity = request.Quantity,
                    UserId = user.Id
                });
                _unitOfWork.SaveChanges();
                return new AddCartItemResponse
                {
                    Message = "Add cart item successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new AddCartItemResponse
                {
                    Message = "Can't add cart item!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }

        }

        public async Task<GetCartItemByUserResponse> GetCartItemByUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new GetCartItemByUserResponse
                {
                    Message = "User not found!",
                    NumberOfItems = 0
                };
            }

            var cartItems = user.CartItems;
            if (cartItems.Count() == 0)
            {
                return new GetCartItemByUserResponse
                {
                    Message = "No item!",
                    NumberOfItems = 0
                };
            }

            return new GetCartItemByUserResponse
            {
                Message = "Success",
                NumberOfItems = cartItems.Count(),
                CartItems = cartItems.Select(item => new GetCartItemByUserResponseItem
                {
                    CartItemId = item.CartItemId.ToString(),
                    ProductId = item.ProductId.ToString(),
                    ProductImage = item.Product.Image,
                    ProductName = item.Product.Name,
                    ProductPrice = item.Product.Price,
                    Quantity = item.Quantity
                })
            };
        }

        public RemoveCartItemResponse RemoveFromCart(string itemId)
        {
            var cartItem = _unitOfWork.CartItemRepository.GetId(Guid.Parse(itemId));
            if (cartItem == null)
            {
                return new RemoveCartItemResponse
                {
                    Message = "Cart item not found!",
                    IsSuccess = false
                };
            }

            try
            {
                _unitOfWork.CartItemRepository.Remove(cartItem);
                _unitOfWork.SaveChanges();
                return new RemoveCartItemResponse
                {
                    Message = "Remove cart item successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new RemoveCartItemResponse
                {
                    Message = "Can't remove cart item!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }
        }

        public UpdateCartItemResponse UpdateCartItem(string itemId, UpdateCartItemRequest request)
        {
            if (request == null)
            {
                return new UpdateCartItemResponse
                {
                    Message = "Update cart request is null!",
                    IsSuccess = false
                };
            }

            var cartItem = _unitOfWork.CartItemRepository.GetId(Guid.Parse(itemId));
            if (cartItem == null)
            {
                return new UpdateCartItemResponse
                {
                    Message = "Cart item not found!",
                    IsSuccess = false
                };
            }

            try
            {
                cartItem.Quantity = request.Quantity;
                _unitOfWork.CartItemRepository.Update(cartItem);
                _unitOfWork.SaveChanges();
                return new UpdateCartItemResponse
                {
                    Message = "Update cart item successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new UpdateCartItemResponse
                {
                    Message = "Can't update cart item!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }
        }
    }
}
