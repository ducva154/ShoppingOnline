using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ShoppingOnline.DTO.Models.Request.Image;
using Microsoft.Extensions.Configuration;

namespace ShoppingOnline.BLL.Services.Impl
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ImageUploadResult> UploadImageAsync(UploadImageRequest request)
        {
            var account = new Account
            {
                Cloud = _configuration["Cloud:Cloud"],
                ApiKey = _configuration["Cloud:ApiKey"],
                ApiSecret = _configuration["Cloud:ApiSecret"]
            };
            var cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription($@"data:image/png;base64,{request.Base64}")
            };

            var uploadResult =  await cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }
    }
}
