using System.ComponentModel.DataAnnotations;

namespace Eagles_Website.ViewModels.LoginRegisterViewModel
{
    public class LoggedInCustomer
    {
        [MinLength(5)]
        public string UserName { get; set; }
        [RegularExpression("[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]{5,}\\.[a-zA-Z]{3,}"
        , ErrorMessage = "Not Valid")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
