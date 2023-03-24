using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Entities
{
    public class CustomUser : IdentityUser
    {
        [StringLength(50)]
        public string? FullName { get; set; }
        [StringLength(255)]
        public string? Avatar { get; set; }
        [DefaultValue(true)]
        public bool? Status { get; set; }
    }
}
