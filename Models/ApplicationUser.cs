using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Eagles_Website.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        public string? Address { get; set; }
        public int ZipCode { get; set; }

        //Navigation Properties
        public Cart? Cart { get; set; }

    }
}
