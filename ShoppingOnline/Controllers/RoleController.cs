using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.BLL.Services;
using ShoppingOnline.BLL.Services.Impl;
using ShoppingOnline.DTO.Models.Request.Role;

namespace ShoppingOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRole()
        {
            var response = await _roleService.GetAllRoles();
            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleService.AddRole(request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] UpdateRoleRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleService.UpdateRole(id, request);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response);
            }
            return BadRequest("Some properties are not valid!");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var response = await _roleService.DeleteRole(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
