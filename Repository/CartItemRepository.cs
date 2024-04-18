using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;

namespace Eagles_Website.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly Context _dbcontext;
        public CartItemRepository(Context dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }

    
    }
}
