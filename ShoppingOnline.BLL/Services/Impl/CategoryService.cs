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
                return new AddCategoryResponse
                {
                    Message = "Add category request is null!",
                    IsSuccess = false,
                };
            }

            try
            {
                Category category = new Category
                {
                    Name = request.Name,
                };
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

        public DeleteCategoryResponse DeleteCategory(string categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetId(Guid.Parse(categoryId));
            if (category == null)
            {
                return new DeleteCategoryResponse
                {
                    Message = "Category not found!",
                    IsSuccess = false,
                };
            }

            try
            {
                _unitOfWork.CategoryRepository.Remove(category);
                _unitOfWork.SaveChanges();
                return new DeleteCategoryResponse
                {
                    Message = "Delete category successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new DeleteCategoryResponse
                {
                    Message = "Can't delete category!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }
        }

        public GetAllCategoryResponse GetAllCategory()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            if (categories.Count() == 0)
            {
                return new GetAllCategoryResponse
                {
                    Message = "No item!",
                    NumberOfItems = 0
                };
            }

            return new GetAllCategoryResponse
            {
                NumberOfItems = categories.Count(),
                Categories = categories.Select(category => new GetAllCategoryResponseItem
                {
                    CategoryId = category.CategoryId.ToString(),
                    CategoryName = category.Name
                })
            };
        }

        public GetCategoryDetailRepsonse GetCategoryDetail(string categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetId(Guid.Parse(categoryId));
            if (category == null)
            {
                return new GetCategoryDetailRepsonse
                {
                    Message = "Category not found!"
                };
            }

            return new GetCategoryDetailRepsonse
            {
                Category = new CategoryDetailResponse
                {
                    CategoryId = categoryId,
                    Name = category.Name
                }
            };
        }

        public UpdateCategoryResponse UpdateCategory(string categoryId, UpdateCategoryRequest request)
        {
            if (request == null)
            {
                return new UpdateCategoryResponse
                {
                    Message = "Update category request is null!",
                    IsSuccess = false
                };
            }

            var category = _unitOfWork.CategoryRepository.GetId(Guid.Parse(categoryId));
            if (category == null)
            {
                return new UpdateCategoryResponse
                {
                    Message = "Category not found!",
                    IsSuccess = false
                };
            }

            try
            {
                category.Name = request.Name;
                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.SaveChanges();
                return new UpdateCategoryResponse
                {
                    Message = "Update category successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new UpdateCategoryResponse
                {
                    Message = "Can't update category!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }
        }
    }
}
