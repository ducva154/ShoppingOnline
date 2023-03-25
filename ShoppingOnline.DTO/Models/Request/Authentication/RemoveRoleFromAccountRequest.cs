using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Request.Authentication
{
    public class RemoveRoleFromAccountRequest
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
