using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using System;

namespace Eagles_Website.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly Context _dbcontext;
        public CategoryRepository(Context dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }

       
    }
}
