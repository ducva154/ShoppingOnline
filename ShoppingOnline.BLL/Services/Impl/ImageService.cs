using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ShoppingOnline.DTO.Models.Request.Image;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var account = new Account
            {
                Cloud = _configuration["Cloud:Cloud"],
                ApiKey = _configuration["Cloud:ApiKey"],
                ApiSecret = _configuration["Cloud:ApiSecret"]
            };
            var cloudinary = new Cloudinary(account);

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream)
            };

            var uploadResult =  await cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }
    }
}
