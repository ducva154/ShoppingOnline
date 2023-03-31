using ShoppingOnline.DTO.Models.Request.Authentication;
using ShoppingOnline.DTO.Models.Request.Role;
using ShoppingOnline.DTO.Models.Response.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
        Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request);
        Task<LoginResponse> LoginUserAsync(LoginRequest request);
        Task<AddRoleToAccountResponse> AddRoleToAccountAsync(AddRoleToAccountRequest request);
        Task<RemoveRoleFromAccountResponse> RemoveRoleFromAccountAsync(RemoveRoleFromAccountRequest request);
        Task<EditProfileResponse> EditProfileAsync(string userId, EditProfileRequest request);
        Task<ChangePasswordResponse> ChangePasswordAsync(string userId, ChangePasswordRequest request);
        Task<GetAllUserResponse> GetAllUserAsync();
        Task<GetUserDetailResponse> GetUserDetailAsync(string userId);

        // SendEmailVerify
        Task<ConfirmEmailResponse> ConfirmEmailAsync(string userId, string token);
        // Send SMS verify
        // ConfirmPhoneNumber
        // Forgot pasword
    }
}
