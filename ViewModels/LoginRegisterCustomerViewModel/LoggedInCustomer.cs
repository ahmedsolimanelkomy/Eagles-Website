using System.ComponentModel.DataAnnotations;

namespace Eagles_Website.ViewModels.LoginRegisterViewModel
{
    public class LoggedInCustomer
    {
        [MinLength(5)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
