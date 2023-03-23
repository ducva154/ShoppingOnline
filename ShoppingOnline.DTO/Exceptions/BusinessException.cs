using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Exceptions
{
    public class BusinessException : Exception
    {
        public int StatusCode { get; set; }

        public BusinessException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public BusinessException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
