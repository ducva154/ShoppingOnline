using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.BLL.Services;
using ShoppingOnline.BLL.Services.Impl;
using ShoppingOnline.DTO.Models.Request.Review;

namespace ShoppingOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("Add/{productId}")]
        public async Task<IActionResult> AddReview(string productId, AddReviewRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _reviewService.AddReview(productId, request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpGet("GetByProduct/{productId}")]
        public IActionResult GetReviewByProduct(string productId)
        {
            var response = _reviewService.GetReviewByProduct(productId);
            return Ok(response);
        }
    }
}
