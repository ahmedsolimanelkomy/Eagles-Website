using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eagles_Website.ViewModels.LoginRegisterViewModel
{
    public class RegisteredCustomer
    {
        [MinLength(5)]
        public string UserName { get; set; }
        [RegularExpression("[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]{5,}\\.[a-zA-Z]{3,}"
        , ErrorMessage = "Not Valid")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        [MinLength(10)]
        public string PhoneNumber { get; set; }
    }
}
