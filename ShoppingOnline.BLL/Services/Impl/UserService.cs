using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoppingOnline.DTO.Constants;
using ShoppingOnline.DTO.Entities;
using ShoppingOnline.DTO.Exceptions;
using ShoppingOnline.DTO.Models.Request.Authentication;
using ShoppingOnline.DTO.Models.Response.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
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
                    if (!identityUser.EmailConfirmed)
                    {
                        var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                        string url = $"{_configuration["Host"]}/api/Authentication/ConfirmEmail?{identityUser.Id}&{confirmEmailToken}";
                        string subject = "CONFIRM EMAIL";
                        string textContent = "Please confirm your email by ";
                        string htmlContent = $"<a href='{url}'>Click here</a>";
                        await _emailService.SendEmailAsync(identityUser.Email, subject, textContent, htmlContent);
                    }

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

        public async Task<ConfirmEmailResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ConfirmEmailResponse
                {
                    Message = "User not found!",
                    IsSuccess = false
                };
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return new ConfirmEmailResponse
                {
                    Message = "Confirm email successfully",
                    IsSuccess = true
                };
            }

            return new ConfirmEmailResponse
            {
                Message = "Can't confirm email!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }

        public async Task<AddRoleToAccountResponse> AddRoleToAccountAsync(AddRoleToAccountRequest request)
        {
            if (request == null)
            {
                return new AddRoleToAccountResponse
                {
                    Message = "Request is null!",
                    IsSuccess = false,
                };
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new AddRoleToAccountResponse
                {
                    Message = "User not found!",
                    IsSuccess = false,
                };
            }

            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
            {
                return new AddRoleToAccountResponse
                {
                    Message = "Role not found!",
                    IsSuccess = false,
                };
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                return new AddRoleToAccountResponse
                {
                    Message = "Add Role to Account successfully!",
                    IsSuccess = true,
                };
            }

            return new AddRoleToAccountResponse
            {
                Message = "Can't add role to account",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<RemoveRoleFromAccountResponse> RemoveRoleFromAccountAsync(RemoveRoleFromAccountRequest request)
        {
            if (request == null)
            {
                return new RemoveRoleFromAccountResponse
                {
                    Message = "Request is null!",
                    IsSuccess = false,
                };
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new RemoveRoleFromAccountResponse
                {
                    Message = "User not found!",
                    IsSuccess = false,
                };
            }

            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
            {
                return new RemoveRoleFromAccountResponse
                {
                    Message = "Role not found!",
                    IsSuccess = false,
                };
            }

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                return new RemoveRoleFromAccountResponse
                {
                    Message = "Remove role from account successfully!",
                    IsSuccess = true,
                };
            }

            return new RemoveRoleFromAccountResponse
            {
                Message = "Can't remove role from account",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<EditProfileResponse> EditProfileAsync(string userId, EditProfileRequest request)
        {
            if (request == null)
            {
                return new EditProfileResponse
                {
                    Message = "Request is null!",
                    IsSuccess = false,
                };
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new EditProfileResponse
                {
                    Message = "User not found!",
                    IsSuccess = false,
                };
            }

            user.FullName = request.FullName;
            user.Avatar = request.Avatar;
            user.PhoneNumber = request.Contact;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new EditProfileResponse
                {
                    Message = "Update profile successfully!",
                    IsSuccess = true,
                };
            }

            return new EditProfileResponse
            {
                Message = "Can't update profile!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<ChangePasswordResponse> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            if (request == null)
            {
                return new ChangePasswordResponse
                {
                    Message = "Request is null!",
                    IsSuccess = false,
                };
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ChangePasswordResponse
                {
                    Message = "User not found!",
                    IsSuccess = false,
                };
            }

            if (!request.NewPassword.Equals(request.ConfirmPassword))
            {
                return new ChangePasswordResponse
                {
                    Message = "Confirm password doesn't match!",
                    IsSuccess = false,
                };
            }

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (result.Succeeded)
            {
                return new ChangePasswordResponse
                {
                    Message = "Change password successfully!",
                    IsSuccess = true,
                };
            }

            return new ChangePasswordResponse
            {
                Message = "Can't change password!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<GetAllUserResponse> GetAllUserAsync()
        {
            return new GetAllUserResponse
            {
                Users = await _userManager.Users.Select(u => new GetUserDetailResponse
                {
                    UserId = u.Id,
                    Avatar = u.Avatar,
                    FullName = u.FullName,
                    Email = u.Email,
                    Contact = u.PhoneNumber,
                    Status = u.Status.HasValue ? u.Status.Value : true,
                    VerifyEmail = u.EmailConfirmed,
                    VerifyPhone = u.PhoneNumberConfirmed
                }).ToListAsync(),
            };
        }

        public async Task<GetUserDetailResponse> GetUserDetailAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            return new GetUserDetailResponse
            {
                UserId = userId,
                Avatar = user.Avatar,
                FullName = user.FullName,
                Email = user.Email,
                Contact = user.PhoneNumber,
                Roles = roles,
                Status = user.Status.HasValue ? user.Status.Value : true,
                VerifyEmail = user.EmailConfirmed,
                VerifyPhone = user.PhoneNumberConfirmed
            };
        }
    }
}
