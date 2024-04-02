using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eagles_Website.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }


        [StringLength(50, MinimumLength = 2, ErrorMessage = "Product Name length Must be from 2 to 50 char "), Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(500, MinimumLength = 5, ErrorMessage = "Product Name length Must be from 5 to 50 char ")]
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Category"),Required]
        public int CategoryId { get; set; }

        //Navigation Properties
        public Category? Category { get; set; }

        [NotMapped]
        public IFormFile? Image { set; get; }






    }
}