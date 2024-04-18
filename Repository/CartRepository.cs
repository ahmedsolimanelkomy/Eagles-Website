using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eagles_Website.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly Context _dbcontext;
        public CartRepository(Context dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
        public Cart GetCartwithincludes(Expression<Func<Cart, bool>> filter)
        {
            return _dbcontext.Carts.Include(c => c.CartItems).ThenInclude(c => c.Product).FirstOrDefault(filter);
        } 
        public Cart handletotal(Cart c)
        {
            foreach(var item in c.CartItems)
            {
                c.TotalPrice += item.SubTotal;
            }
            return c;
        }
        
    }
}
