using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingOnline.DTO.Constants;
using ShoppingOnline.DTO.Entities;
using ShoppingOnline.DTO.Exceptions;
using ShoppingOnline.DTO.Models.Request.Authentication;
using ShoppingOnline.DTO.Models.Response.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
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

            var identityUser = new CustomUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = request.VerifyEmail.HasValue ? request.VerifyEmail.Value : false,
                PhoneNumber = request.Contact,
                PhoneNumberConfirmed = request.VerifyContact.HasValue ? request.VerifyContact.Value : false,
                Avatar = request.Avatar,
                Status = request.Status.HasValue ? request.Status : true,
                FullName = request.FullName
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (result.Succeeded)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(identityUser, request.RoleName.IsNullOrEmpty() ? RoleConstant.USER : request.RoleName);
                if (addRoleResult.Succeeded)
                {
                    return new RegisterResponse
                    {
                        Message = "Create user successfully!",
                        IsSuccess = true
                    };
                }
                return new RegisterResponse
                {
                    Message = "Can't add role to user!",
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

        public async Task<LoginResponse> LoginUserAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return new LoginResponse
                {
                    Message = "User name is not exist!",
                    IsSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return new LoginResponse
                {
                    Message = "Wrong password!",
                    IsSuccess = false
                };
            }

            var claims = new[]
            {
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
                new Claim("Id", user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(TokenExpireTimeConstant.EXPIRE_TIME_ON_DAY),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse
            {
                Message = "Login successfully!",
                IsSuccess = true,
                Token = tokenAsString
            };
        }
    }
}
