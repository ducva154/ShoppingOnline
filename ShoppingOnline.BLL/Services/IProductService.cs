using ShoppingOnline.DTO.Models.Request.Product;
using ShoppingOnline.DTO.Models.Response.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services
{
    public interface IProductService
    {
        CreateProductResponse CreateProduct(CreateProductRequest request);
        UpdateProductResponse UpdateProduct(string productId, UpdateProductRequest request);
        DeleteProductResponse DeleteProduct(string productId);
        GetAllProductResponse GetAllProduct();
        GetProductDetailResponse GetProductDetail(string productId);
        GetProductByCategoryResponse GetProductByCategory(string categoryId);
    }
}
