using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eagles_Website.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cart")]
        public int CartID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal SubTotal { get; set; }

        //Navigation properties
        public Cart? Cart { get; set; }

        public Product? Product { get; set; }
    }
}
