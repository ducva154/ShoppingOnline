using ShoppingOnline.DTO.Migrations;
using ShoppingOnline.DTO.Models.Request;
using ShoppingOnline.DTO.Models.Response.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services
{
    public interface IReviewService
    {
        // Get review by product
        GetReviewByProductResponse GetReviewByProduct(string productId);
        // Add Review - userId, productId
        Task<AddReviewResponse> AddReview(string productId, AddReviewRequest request);
    }
}
