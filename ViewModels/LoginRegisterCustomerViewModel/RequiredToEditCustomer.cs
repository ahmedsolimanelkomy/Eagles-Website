using System.ComponentModel.DataAnnotations;

namespace Eagles_Website.ViewModels.LoginRegisterCustomerViewModel
{
    public class RequiredToEditCustomer
    {
        [MinLength(5,ErrorMessage ="Should be more than 5 characters")]
        public string FullName{ get; set; }
        [RegularExpression("[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]{5,}\\.[a-zA-Z]{3,}"
,        ErrorMessage = "Not Valid")]
        [DataType(DataType.EmailAddress)]
        
        public string? NewEmail{ get; set; }
        [MinLength(10,ErrorMessage ="should be more than 10 numbers")]
        public string PhoneNumber{ get; set; }
        public string Address{ get; set; }
        [Required(ErrorMessage ="You should enter the password")]
        [DataType(DataType.Password)]
        public string? OldPassword{ get; set; }
        [Display(Name = "Enter the new Password")]
        [DataType(DataType.Password)]
        public string? NewPassword{ get; set; }
        [Compare("NewPassword",ErrorMessage ="Must be equal to the New Password")]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmNewPassword{  get; set; }

    }
}
