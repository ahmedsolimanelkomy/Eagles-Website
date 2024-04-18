namespace Eagles_Website.ViewModels
{
    public class OrderViewModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        public int TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public int CartID { get; set; }
    }
}
