using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eagles_Website.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("ApplicationUser")]
        public int? UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public int? TotalAmount { get; set; }

        //Navigation properties
        public Payment? Payment { get; set; }

        public List<OrderDetails>? OrderDetails { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }
    }
}

