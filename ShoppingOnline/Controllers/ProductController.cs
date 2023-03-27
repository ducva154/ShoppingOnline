using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.BLL.Services;
using ShoppingOnline.BLL.Services.Impl;
using ShoppingOnline.DTO.Models.Request.Product;

namespace ShoppingOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("Add")]
        public IActionResult AddProduct([FromBody] CreateProductRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = _productService.CreateProduct(request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpPut("Update/{productId}")]
        public IActionResult UpdateProduct(string productId, [FromBody] UpdateProductRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = _productService.UpdateProduct(productId, request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpDelete("Delete/{productId}")]
        public IActionResult DeleteProduct(string productId)
        {
            var response = _productService.DeleteProduct(productId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            var response = _productService.GetAllProduct();
            return Ok(response);
        }

        [HttpGet("GetProductDetail/{productid}")]
        public IActionResult GetProductDetail(string productId)
        {
            var response = _productService.GetProductDetail(productId);
            return Ok(response);
        }

        [HttpGet("GetProductByCategory/{categoryId}")]
        public IActionResult GetProductByCategory(string categoryId)
        {
            var response = _productService.GetProductByCategory(categoryId);
            return Ok(response);
        }
    }
}
