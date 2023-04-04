using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using ShoppingOnline.DTO.Models.Request.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.BLL.Services
{
    public interface IImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
    }
}
