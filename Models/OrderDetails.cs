using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace Eagles_Website.Models
{
    public class OrderDetails
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int SubTotal { get; set; }

        //Navigation properties
        public Order? Order { get; set; }

        public Product? Product { get; set; }
    }
}
