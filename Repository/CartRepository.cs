using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
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

        
    }
}
