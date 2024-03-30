using Eagles_Website.Models;
using Eagles_Website.ViewModels;
using System.Linq.Expressions;

namespace Eagles_Website.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        public ProductCategoryViewModel GetProductWithCategories();
       
    }
}
