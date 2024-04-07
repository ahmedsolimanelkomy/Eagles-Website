using System.ComponentModel.DataAnnotations;

namespace Eagles_Website.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category Name length Must be from 2 to 50 char "),Required]

        public string Name { get; set; }

        [StringLength(500, MinimumLength = 5, ErrorMessage = "Category Name length Must be from 5 to 50 char ")]

        public string? Description { get; set; }


        //Navigtion Properties
        public List<Product>? Products { get; set; }


    }
}
