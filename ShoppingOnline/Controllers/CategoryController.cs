using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.BLL.Services;
using ShoppingOnline.BLL.Services.Impl;
using ShoppingOnline.DTO.Models.Request.Category;

namespace ShoppingOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Add")]
        public IActionResult AddCategory([FromBody] AddCategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = _categoryService.AddCategory(request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpPut("Update/{categoryId}")]
        public IActionResult UpdateCategory(string categoryId, [FromBody] UpdateCategoryRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = _categoryService.UpdateCategory(categoryId, request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpDelete("Delete/{categoryId}")]
        public IActionResult DeleteCategory(string categoryId)
        {
            var response = _categoryService.DeleteCategory(categoryId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllCategory()
        {
            var response = _categoryService.GetAllCategory();
            return Ok(response);
        }

        [HttpGet("GetDetail/{categoryId}")]
        public IActionResult GetCategoryDetail(string categoryId)
        {
            var response = _categoryService.GetCategoryDetail(categoryId);
            return Ok(response);
        }
    }
}
