using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Request.Image
{
    public class UploadImageRequest
    {
        public IFormFile Image { get; set; }
    }
}
