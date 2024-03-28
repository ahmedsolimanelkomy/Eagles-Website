using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;

namespace Eagles_Website.Repository
{
    public class OrderRepository:Repository<Order>, IOrderRepository
    {
        private readonly Context _dbcontext;
        public OrderRepository(Context dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
