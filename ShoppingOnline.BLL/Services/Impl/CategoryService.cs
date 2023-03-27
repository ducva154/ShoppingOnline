using Microsoft.AspNetCore.Identity;
using ShoppingOnline.DAL.Repositories;
using ShoppingOnline.DTO.Entities;
using ShoppingOnline.DTO.Exceptions;
using ShoppingOnline.DTO.Models.Request.Category;
using ShoppingOnline.DTO.Models.Response.Authentication;
using ShoppingOnline.DTO.Models.Response.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AddCategoryResponse AddCategory(AddCategoryRequest request)
        {
            if (request == null)
            {
                throw new BusinessException("Add category request is null!");
            }

            Category category = new Category
            {
                Name = request.Name,
            };
            try
            {
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.SaveChanges();
                return new AddCategoryResponse
                {
                    Message = "Create category successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new AddCategoryResponse
                {
                    Message = "Can't create category!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }
        }
    }
}
