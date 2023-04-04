using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DAL.Repositories.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IReviewRepository _reviewRepository;
        public UnitOfWork(DbContext context, IProductRepository productRepository,
                            ICategoryRepository categoryRepository,
                            IOrderRepository orderRepository,
                            IOrderDetailRepository orderDetailRepository,
                            ICartItemRepository cartItemRepository,
                            IReviewRepository reviewRository)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _cartItemRepository = cartItemRepository;
            _reviewRepository = reviewRository;
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository; }
        }

        public ICategoryRepository CategoryRepository
        {
            get { return _categoryRepository; }
        }

        public IOrderDetailRepository OrderDetailRepository
        {
            get { return _orderDetailRepository; }
        }

        public IOrderRepository OrderRepository
        {
            get { return _orderRepository; }
        }

        public ICartItemRepository CartItemRepository
        {
            get { return _cartItemRepository; }
        }

        public IReviewRepository ReviewRepository
        {
            get { return _reviewRepository; }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        void IUnitOfWork.SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
