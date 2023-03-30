using Microsoft.AspNetCore.Identity;
using ShoppingOnline.DAL.Repositories;
using ShoppingOnline.DTO.Entities;
using ShoppingOnline.DTO.Models.Request;
using ShoppingOnline.DTO.Models.Response.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<CustomUser> _userManager;

        public ReviewService(IUnitOfWork unitOfWork, UserManager<CustomUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<AddReviewResponse> AddReview(string productId, AddReviewRequest request)
        {
            if (request == null)
            {
                return new AddReviewResponse
                {
                    Message = "Add review request is null!",
                    IsSuccess = false
                };
            }

            var product = _unitOfWork.ProductRepository.GetId(Guid.Parse(productId));
            if (product == null)
            {
                return new AddReviewResponse
                {
                    Message = "Product not found!",
                    IsSuccess = false
                };
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new AddReviewResponse
                {
                    Message = "User not found!",
                    IsSuccess = false
                };
            }

            try
            {
                var review = new Review
                {
                    ProductId = product.ProductId,
                    UserId = request.UserId,
                    Rating = request.Rating,
                    Content = request.Content,
                    Date = DateTime.Now,
                };
                _unitOfWork.ReviewRepository.Add(review);

                var rating = product.Rating;
                rating = (rating * (product.Reviews.Count() - 1) + review.Rating) / product.Reviews.Count();
                product.Rating = rating;
                _unitOfWork.ProductRepository.Update(product);

                _unitOfWork.SaveChanges();
                return new AddReviewResponse
                {
                    Message = "Add review successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new AddReviewResponse
                {
                    Message = "Can't add review!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }
        }

        public GetReviewByProductResponse GetReviewByProduct(string productId)
        {
            var product = _unitOfWork.ProductRepository.GetId(Guid.Parse(productId));
            if (product == null)
            {
                return new GetReviewByProductResponse
                {
                    Message = "Product not found!",
                    NumberOfItems = 0
                };
            }

            var reviews = product.Reviews;
            if (reviews.Count() == 0)
            {
                return new GetReviewByProductResponse
                {
                    Message = "No item!",
                    NumberOfItems = 0
                };
            }

            return new GetReviewByProductResponse
            {
                Message = "Success!",
                NumberOfItems = reviews.Count(),
                Reviews = reviews.Select(review => new GetReviewItem
                {
                    UserAvatar = review.User.Avatar,
                    UserFullName = review.User.FullName,
                    Rating = review.Rating,
                    Content = review.Content,
                    Date = review.Date
                })
            };
        }
    }
}
