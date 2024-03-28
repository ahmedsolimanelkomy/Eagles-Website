using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;

namespace Eagles_Website.Repository
{
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private readonly Context _dbcontext;
        public OrderDetailRepository(Context dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    
        
    }
}
