using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eagles_Website.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        public string? Address { get; set; }
        public int ZipCode { get; set; }
        [ForeignKey("Cart")]
        public int? CartId { get; set; }
        //Navigation Property
        public Cart? Cart { get; set; }
    }
}
