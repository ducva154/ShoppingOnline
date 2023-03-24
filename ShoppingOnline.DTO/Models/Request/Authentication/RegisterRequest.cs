using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Request.Authentication
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
        [StringLength(50)]
        public string? RoleName { get; set; }
        [StringLength(50)]
        public string? FullName { get; set; }
        [StringLength(255)]
        public string? Avatar { get; set; }
        [StringLength(20)]
        public string? Contact { get; set; }
        public bool? Status { get; set; }
        public bool? VerifyEmail { get; set; }
        public bool? VerifyContact { get; set; }
    }
}
