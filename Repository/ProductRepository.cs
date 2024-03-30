using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using System.Linq.Expressions;
using System;
using Eagles_Website.ViewModels;

namespace Eagles_Website.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        Context dbcontext;
        public ProductRepository(Context _dbcontext) : base(_dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public ProductCategoryViewModel GetProductWithCategories()
        {
            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel()
            {
               Product = new Product { },
               Categories = dbcontext.Categories.ToList()
            };

            return productCategoryViewModel;
        }
    }
}
