using ShoppingOnline.DAL.Repositories;
using ShoppingOnline.DTO.Entities;
using ShoppingOnline.DTO.Models.Request.Image;
using ShoppingOnline.DTO.Models.Request.Product;
using ShoppingOnline.DTO.Models.Response.Product;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public ProductService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public async Task<CreateProductResponse> CreateProduct(CreateProductRequest request)
        {
            if (request == null)
            {
                return new CreateProductResponse
                {
                    Message = "Create product request is null!",
                    IsSuccess = false
                };
            }

            try
            {
                var uploadResult = await _imageService.UploadImageAsync(request.Image);
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Discount = request.Discount,
                    Brand = request.Brand,
                    StockQuantity = request.StockQuantity,
                    Image = uploadResult.Uri.ToString(),
                    Rating = request.Rating,
                    Categories = request.CategoryIds.Select(cid => _unitOfWork.CategoryRepository.GetId(Guid.Parse(cid))).ToList(),
                };
                _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.SaveChanges();
                return new CreateProductResponse
                {
                    Message = "Create product successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new CreateProductResponse
                {
                    Message = "Can't create product!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }

        }

        public async Task<UpdateProductResponse> UpdateProduct(string productId, UpdateProductRequest request)
        {
            if (request == null)
            {
                return new UpdateProductResponse
                {
                    Message = "Update product request is null!",
                    IsSuccess = false
                };
            }

            var product = _unitOfWork.ProductRepository.GetId(Guid.Parse(productId));
            if (product == null)
            {
                return new UpdateProductResponse
                {
                    Message = "Product not found!",
                    IsSuccess = false
                };
            }

            try
            {
                var uploadResult = await _imageService.UploadImageAsync(request.Image);
                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.Discount = request.Discount;
                product.Brand = request.Brand;
                product.StockQuantity = request.StockQuantity;
                product.Image = uploadResult.Uri.ToString();
                product.Rating = request.Rating;
                //product.Categories = request.CategoryIds.Select(cid => _unitOfWork.CategoryRepository.GetId(Guid.Parse(cid)));
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.SaveChanges();
                return new UpdateProductResponse
                {
                    Message = "Update product successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new UpdateProductResponse
                {
                    Message = "Can't update product!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }
        }

        public DeleteProductResponse DeleteProduct(string productId)
        {
            var product = _unitOfWork.ProductRepository.GetId(Guid.Parse(productId));
            if (product == null)
            {
                return new DeleteProductResponse
                {
                    Message = "Product not found!",
                    IsSuccess = false
                };
            }

            try
            {
                _unitOfWork.ProductRepository.Remove(product);
                _unitOfWork.SaveChanges();
                return new DeleteProductResponse
                {
                    Message = "Delete product successfully!",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new DeleteProductResponse
                {
                    Message = "Can't delete product!",
                    IsSuccess = false,
                    Error = ex.Message
                };
            }
        }

        public GetAllProductResponse GetAllProduct()
        {
            var products = _unitOfWork.ProductRepository.GetAll();
            if (products.Count() == 0)
            {
                return new GetAllProductResponse
                {
                    Message = "No item!",
                    NumberOfItems = products.Count()
                };
            }
            return new GetAllProductResponse
            {
                NumberOfItems = products.Count(),
                Products = products.Select(product => new GetAllProductItemResponse
                {
                    Image = product.Image,
                    Name = product.Name,
                    Brand = product.Brand,
                    ProductId = product.ProductId.ToString(),
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    Rating = product.Rating,
                    Categories = product.Categories.Select(category => category.Name)
                })
            };
        }

        public GetProductByCategoryResponse GetProductByCategory(string categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetId(Guid.Parse(categoryId));
            if (category == null)
            {
                return new GetProductByCategoryResponse
                {
                    Message = "Category not found!",
                    NumberOfItems = 0
                };
            }

            //var products = _unitOfWork.ProductRepository.GetListProductByCategory(category);
            var products = category.Products;
            if (products.Count() == 0)
            {
                return new GetProductByCategoryResponse
                {
                    Message = "No item!",
                    NumberOfItems = 0
                };
            }

            return new GetProductByCategoryResponse
            {
                NumberOfItems = products.Count(),
                Products = products.Select(product => new GetProductByCategoryItemResponse
                {
                    Imange = product.Image,
                    Name = product.Name,
                    ProductId = product.ProductId.ToString(),
                    Price = product.Price,
                    Discount = product.Discount,
                    IsAvailable = product.StockQuantity > 0,
                })
            };
        }

        public GetProductDetailResponse GetProductDetail(string productId)
        {
            var product = _unitOfWork.ProductRepository.GetId(Guid.Parse(productId));
            if (product == null)
            {
                return new GetProductDetailResponse
                {
                    Message = "Product not found!"
                };
            }

            return new GetProductDetailResponse
            {
                Product = new ProductDetailResponse
                {
                    ProductId = productId,
                    Name = product.Name,
                    Image = product.Image,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    Brand = product.Brand,
                    StockQuantity = product.StockQuantity,
                    Rating = product.Rating,
                    //CategoryIds = _unitOfWork.CategoryRepository.GetListCategoriesByProduct(product).Select(category => category.CategoryId.ToString())
                    CategoryIds = product.Categories.Select(category => category.CategoryId.ToString())
                }
            };
        }
    }
}
