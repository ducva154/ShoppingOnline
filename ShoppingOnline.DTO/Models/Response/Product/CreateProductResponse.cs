using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Product
{
    public class CreateProductResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }
}
