using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingOnline.DTO.Models.Request.Authentication;
using ShoppingOnline.DTO.Models.Request.Category;
using ShoppingOnline.DTO.Models.Request.Role;
using ShoppingOnline.DTO.Models.Response.Authentication;
using ShoppingOnline.DTO.Models.Response.Category;
using ShoppingOnline.DTO.Models.Response.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services
{
    public interface IRoleService
    {
        Task<GetListRoleResponse>  GetAllRoles();
        Task<AddRoleResponse> AddRole([FromBody]AddRoleRequest request);
        Task<UpdateRoleResponse> UpdateRole(string id, [FromBody] UpdateRoleRequest request);
        Task<DeleteRoleResponse> DeleteRole(string id);
    }
}
