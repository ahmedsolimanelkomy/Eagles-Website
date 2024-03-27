using System.ComponentModel.DataAnnotations.Schema;

namespace Eagles_Website.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string? Method { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        //Navigation properties
        public Order? Order { get; set; }
    }
}
