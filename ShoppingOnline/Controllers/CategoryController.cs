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
        [Authorize]
        public IActionResult AddController([FromBody] AddCategoryRequest request)
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
    }
}
