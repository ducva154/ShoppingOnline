using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Request.Review
{
    public class AddReviewRequest
    {
        public string UserId { get; set; }
        public double Rating { get; set; }
        public string Content { get; set; }

    }
}
