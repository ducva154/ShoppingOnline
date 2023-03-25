using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Authentication
{
    public class GetUserDetailResponse
    {
        public string UserId { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Contact { get; set; }
        public bool Status { get; set; }
        public bool VerifyEmail { get; set; }
        public bool VerifyPhone { get; set; }
    }
}
