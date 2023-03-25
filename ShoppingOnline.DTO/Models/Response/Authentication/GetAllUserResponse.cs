using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Authentication
{
    public class GetAllUserResponse
    {
        public IEnumerable<GetUserDetailResponse> Users { get; set; }
    }
}
