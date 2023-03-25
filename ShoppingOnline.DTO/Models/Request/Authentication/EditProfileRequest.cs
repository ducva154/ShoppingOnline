using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Request.Authentication
{
    public class EditProfileRequest
    {
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Contact { get; set; }
    }
}
