using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingOnline.DAL.Repositories;
using ShoppingOnline.DTO.Exceptions;
using ShoppingOnline.DTO.Models.Request.Role;
using ShoppingOnline.DTO.Models.Response.Authentication;
using ShoppingOnline.DTO.Models.Response.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GetListRoleResponse> GetAllRoles()
        {
            var roles = await _roleManager.Roles.Select(role => new RoleResponse
            {
                RoleId = role.Id,
                Name = role.Name
            }).ToListAsync();

            return new GetListRoleResponse
            {
                Roles = roles,
            };
        }

        public async Task<AddRoleResponse> AddRole([FromBody] AddRoleRequest request)
        {
            if (request == null)
            {
                throw new BusinessException("Register request is null!");
            }

            var identityRole = new IdentityRole
            {
                Name = request.Name,
            };

            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return new AddRoleResponse
                {
                    Message = "Create user successfully!",
                    IsSuccess = true
                };
            }

            return new AddRoleResponse
            {
                Message = "Can't create user!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UpdateRoleResponse> UpdateRole(string id, [FromBody] UpdateRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return new UpdateRoleResponse
                {
                    Message = "Role name is not exist!",
                    IsSuccess = false
                };
            }

            role.Name = request.Name;

            var result = await _roleManager.UpdateAsync(role);
            
            if (result.Succeeded)
            {
                return new UpdateRoleResponse
                {
                    Message = "Update role successfully!",
                    IsSuccess = true
                };
            }

            return new UpdateRoleResponse
            {
                Message = "Can't update role!",
                IsSuccess = false
            };
        }

        public async Task<DeleteRoleResponse> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return new DeleteRoleResponse
                {
                    Message = "Role name is not exist!",
                    IsSuccess = false
                };
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return new DeleteRoleResponse
                {
                    Message = "Delete role successfully!",
                    IsSuccess = true
                };
            }

            return new DeleteRoleResponse
            {
                Message = "Can't delete role!",
                IsSuccess = false
            };
        }
    }
}
