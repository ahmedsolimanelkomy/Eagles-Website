using Eagles_Website.Models;
using Eagles_Website.Repository;
using Eagles_Website.Repository.IRepository;

namespace Eagles_Website.ViewModels
{
    public class ProductCategoryViewModel
    {
        public Product? Product { get; set; }
        public List<Category> Categories { get; set; }

    }
}
