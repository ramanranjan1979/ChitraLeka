using ChitraLeka.ViewModel.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ChitraLeka.ViewModel.Security
{
    public class Security
    {
        public int LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public bool IsLock { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; } = null;
        public DateTime? LockedOn { get; set; } = null;

        public Contact.Person Person { get; set; }

        public List<string> RoleNameList { get; set; }
    }

    public class NewPeronLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PersonId { get; set; }
        public SelectList people { get; set; }

        public int roleId { get; set; }
        public SelectList roles { get; set; }
    }


    public class SystemLog
    {
        public int Id { get; set; }
        public LogType Type { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public Contact.Person Person { get; set; }
    }

    public class LogType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ChangePassword
    {
        public int loginId { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(10,ErrorMessage ="Password cannot be more than 10 characters")]
        [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter you password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(10, ErrorMessage = "New Password cannot be more than 10 characters")]
        [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter new password")]        
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(10, ErrorMessage = "Old Password cannot be more than 10 characters")]
        [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please confirm your password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "New password and confirm password is not matching")]
        public string ConfirmPassword { get; set; }

    }


    public class SecurityType
    {
        public int Id { get; set; }
        public string Name { get; set; }        
    }

    public class SecurityTypeCode
    {
        public int Id { get; set; }        
        public string Code { get; set; }
        public SecurityType SecurityType { get; set; }
        public Person Person { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ExpiredOn { get; set; }
    }

    public class ResetPassword
    {
        public Person Person { get; set; }
        
        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "Minimum 5 characters are allowed in security code")]
        [MaxLength(10, ErrorMessage = "Maximum 10 characters are allowed in security code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter the security code")]
        public string SecurityCode { get; set; }

 

        [DataType(DataType.Password)]
        [MaxLength(10, ErrorMessage = "Password cannot be more than 10 characters")]
        [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter you password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]        
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "New password and confirm password is not matching")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please confirm your password")]
        public string ConfirmPassword { get; set; }
    }
}