using ShoppingOnline.DTO.Models.Request.Authentication;
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
        // Login
        Task<LoginResponse> LoginUserAsync(LoginRequest request);
        // Add  role
        // Remove role
        // Forgot pasword
        // Edit profile
        // Change password
        // Verify email
        // Verify phone
        // Get all
        // Get detail
    }
}
