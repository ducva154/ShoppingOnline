using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Models.Response.Review
{
    public class GetReviewByProductResponse
    {
        public string Message { get; set; }
        public int NumberOfItems { get; set; }
        public IEnumerable<GetReviewItem> Reviews { get; set; }
    }

    public class GetReviewItem
    {
        public string UserAvatar { get; set; }
        public string UserFullName { get; set; }
        public double Rating { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
