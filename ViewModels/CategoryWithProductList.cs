using Eagles_Website.Models;
namespace Eagles_Website.ViewModels
{
    public class CategoryWithProductList
    {
        public int ID { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<Product>? Products { get; set; }
    }
}
