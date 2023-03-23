using ShoppingOnline.DTO.Models.Request;
using ShoppingOnline.DTO.Models.Response;
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
        // Create account - admin
        // Login
        // Forgot pasword
        // Edit profile
        // Change password
        // Verify email
        // Verify phone
        // Get all
        // Get detail
    }
}
