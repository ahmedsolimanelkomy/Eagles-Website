using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eagles_Website.Models
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("ApplicationUser")]
        public int UserID { get; set; }
        public decimal TotalPrice { get; set; }

        //Navigation Properties
        public ApplicationUser? ApplicationUser { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
