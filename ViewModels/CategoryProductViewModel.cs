using Eagles_Website.Models;

namespace Eagles_Website.ViewModels
{
    public class CategoryProductViewModel
    {
        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set;}
        public int SelectedCatID { get; set; }
    }
}
