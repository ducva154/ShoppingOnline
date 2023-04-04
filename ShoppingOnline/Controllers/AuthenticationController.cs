using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.BLL.Services;
using ShoppingOnline.DTO.Models.Request.Authentication;
using System.Security.Permissions;

namespace ShoppingOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public AuthenticationController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(request);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid!");
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccountAsync([FromForm] CreateAccountRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateAccountAsync(request);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(request);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid!");
        }

        [HttpGet("ConfirmEmail/{userId}/{token}")]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ConfirmEmailAsync(userId, token);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid!");
        }

        [HttpPost("AddRoleToAccount")]
        public async Task<IActionResult> AddRoleToAccountAsync([FromBody] AddRoleToAccountRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AddRoleToAccountAsync(request);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid!");
        }

        [HttpDelete("RemoveRoleFromAccount")]
        public async Task<IActionResult> RemoveRoleFromAccountAsync([FromBody] RemoveRoleFromAccountRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RemoveRoleFromAccountAsync(request);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid!");
        }

        [HttpPut("EditProfile/{userId}")]
        public async Task<IActionResult> EditProfileAsync(string userId, [FromForm] EditProfileRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditProfileAsync(userId, request);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid!");
        }

        [HttpPut("ChangePassword/{userId}")]
        public async Task<IActionResult> ChangePasswordAsync(string userId, [FromBody] ChangePasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangePasswordAsync(userId, request);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid!");
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUserAsync()
        {

            var result = await _userService.GetAllUserAsync();
            return Ok(result);

        }

        [HttpGet("GetUserDetail/{userId}")]
        public async Task<IActionResult> GetUserDetailAsync(string userId)
        {

            var result = await _userService.GetUserDetailAsync(userId);
            return Ok(result);

        }
    }
}
