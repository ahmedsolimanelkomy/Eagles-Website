using Eagles_Website.Models;
using System.Linq.Expressions;

namespace Eagles_Website.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        public Cart GetCartwithincludes(Expression<Func<Cart, bool>> filter);
        public Cart handletotal(Cart c);
    }
}
