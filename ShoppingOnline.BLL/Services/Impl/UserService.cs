using Microsoft.AspNetCore.Identity;
using ShoppingOnline.DTO.Exceptions;
using ShoppingOnline.DTO.Models.Request;
using ShoppingOnline.DTO.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
        {
            if (request == null)
            {
                throw new BusinessException("Register request is null!");
            }

            if (request.Password != request.ConfirmPassword)
            {
                return new RegisterResponse
                {
                    Message = "Confirm password doesn't match password!",
                    IsSuccess = false
                };
            }

            var identityUser = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (result.Succeeded)
            {
                return new RegisterResponse
                {
                    Message = "Create user successfully!",
                    IsSuccess = true
                };
            }

            return new RegisterResponse
            {
                Message = "Can't create user!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
