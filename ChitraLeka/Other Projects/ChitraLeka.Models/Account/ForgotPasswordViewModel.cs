using System.ComponentModel.DataAnnotations;

namespace ChitraLeka.ViewModel.Account
{
    public class ForgotPassword
    {
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your email address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your username")]
        [MaxLength(15, ErrorMessage = "Username cannot be more than 15 characters")]
        [MinLength(5, ErrorMessage = "Username cannot be less than 5 characters")]
        public string UserName { get; set; }
    }

    public class ResetPassword
    {
        public int MemberId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string EmailAddress { get; set; }

        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "INVALID SECURITY CODE")]
        [MaxLength(10, ErrorMessage = "INVALID SECURITY CODE")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ENTER YOUR SECURITY ADDRESS")]
        public string SecurityCode { get; set; }

        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "INVALID SECURITY CODE")]
        [MaxLength(10, ErrorMessage = "INVALID SECURITY CODE")]
        [Compare("SecurityCode", ErrorMessage = "YOU HAVE ENTERED AN INVALID SECURITY CODE")]
        public string ConfirmSecurityCode { get; set; }

        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "PASSWORD MUST BE LENGTH OF 5 OR MORE CHARACTERS")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ENTER YOUR PASSWORD")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "PASSWORD IS NOT THE SAME")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "CONFIRM YOUR PASSWORD")]
        public string ConfirmPassword { get; set; }
    }
}