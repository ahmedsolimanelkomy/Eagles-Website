using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using System;

namespace Eagles_Website.Repository
{
    public class UnitOfWork : IUnitOFWork
    {
        public ICategoryRepository CategoryRepo { get; private set; }

        public IProductRepository ProductRepo { get; private set; }

        public ICartRepository CartRepo { get; private set; }

        public ICartItemRepository CartItemRepo { get; private set; }

        public IOrderRepository OrderRepo { get; private set; }

        public IOrderDetailRepository OrderDetailRepo { get; private set; }

        Context dbcontext;
        public UnitOfWork(Context a)
        {
            dbcontext = a;
            CategoryRepo = new CategoryRepository(dbcontext);
            ProductRepo = new ProductRepository(dbcontext);
            CartRepo = new CartRepository(dbcontext);
            CartItemRepo = new CartItemRepository(dbcontext);
            OrderRepo = new OrderRepository(dbcontext);
            OrderDetailRepo = new OrderDetailRepository(dbcontext);


        }

        
    }
}
