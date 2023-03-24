using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Role
{
    public class GetListRoleResponse
    {
        public IEnumerable<RoleResponse> Roles { get; set; }
    }
}
