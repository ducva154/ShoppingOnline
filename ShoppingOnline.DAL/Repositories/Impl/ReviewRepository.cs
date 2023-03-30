using Microsoft.EntityFrameworkCore;
using ShoppingOnline.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DAL.Repositories.Impl
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(DbContext context) : base(context) { }
    }
}
