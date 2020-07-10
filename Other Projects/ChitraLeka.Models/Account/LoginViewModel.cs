using ChitraLeka.ViewModel.Contact;
using System.ComponentModel.DataAnnotations;

namespace ChitraLeka.ViewModel.Account
{
    public class Login
    {
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter you password")]
        public string Password { get; set; }

        public string NavTo { get; set; }

    }

    public class LoginStatus
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TargetURL { get; set; }
        public int memberId { get; set; }
        public Security.Security LoggedInPerson { get; set; }
    }
}