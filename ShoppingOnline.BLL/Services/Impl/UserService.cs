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
using ShoppingOnline.DTO.Models.Request.Image;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IImageService _imageService;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IImageService imageService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _imageService = imageService;
            _configuration = configuration;
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
        {
            if (request == null)
            {
                return new RegisterResponse
                {
                    Message = "Register request is null!",
                    IsSuccess = false
                };
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
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (result.Succeeded)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(identityUser, RoleConstant.USER);
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

        public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request)
        {
            if (request == null)
            {
                return new CreateAccountResponse
                {
                    Message = "Create account request is null!",
                    IsSuccess = false
                };
            }

            if (request.Password != request.ConfirmPassword)
            {
                return new CreateAccountResponse
                {
                    Message = "Confirm password doesn't match password!",
                    IsSuccess = false
                };
            }
            var uploadResult = await _imageService.UploadImageAsync(request.Avatar);
            var identityUser = new CustomUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = request.VerifyEmail,
                PhoneNumber = request.Contact,
                PhoneNumberConfirmed = request.VerifyContact,
                Avatar = uploadResult.Uri.ToString(),
                Status = request.Status,
                FullName = request.FullName
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if (result.Succeeded)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(identityUser, request.RoleName);
                if (addRoleResult.Succeeded)
                {
                    return new CreateAccountResponse
                    {
                        Message = "Create user successfully!",
                        IsSuccess = true
                    };
                }
                return new CreateAccountResponse
                {
                    Message = "Can't add role to user!",
                    IsSuccess = true
                };
            }

            return new CreateAccountResponse
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
            var uploadResult = await _imageService.UploadImageAsync(request.Avatar);
            user.Avatar = uploadResult.Uri.ToString();
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
    }
}
