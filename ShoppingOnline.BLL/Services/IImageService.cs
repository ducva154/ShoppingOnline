using CloudinaryDotNet.Actions;
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
        Task<ImageUploadResult> UploadImageAsync(UploadImageRequest request);
    }
}
