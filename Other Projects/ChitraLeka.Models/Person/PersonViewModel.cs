using ChitraLeka.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ChitraLeka.ViewModel.Contact
{
    public class BusinessRule
    {
        public string RuleMessage { get; set; } = string.Empty;
    }

    public class BusinessRuleList
    {
        public List<BusinessRule> RuleMessageList { get; set; } 
    }

    public class Person : BusinessRuleList
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [DataType(DataType.Text)]
        public string MiddleName { get; set; }


        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }


        [Display(Name = "Date of birth")]
        [Required]
        // [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]        
        public DateTime DOB { get; set; } = DateTime.Now;

        public string DOBStr { get; set; }



        public DateTime DateCreated { get; set; } = DateTime.Now;

        #region dropdowns
        public string GenderTypeId { get; set; }
        public SelectList Gender { get; set; }

        public string SalutationTypeId { get; set; }
        public SelectList Salutations { get; set; }

        public string PersonTypeId { get; set; }
        public SelectList PersonTypes { get; set; }
        #endregion

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Email Address")]
        [Remote("DoesEmailExist", "EmailAddresses", HttpMethod = "POST", ErrorMessage = "Email address already exists. Please use a different email address.")]
        public string  PrimaryEmail { get; set; }

        public List<EmailAddress> PersonEmail { get; set; }

        public List<Address> PersonAddress { get; set; }
        public List<Contact> PersonContactNumber { get; set; }

    }

    public class EmailAddress : BusinessRuleList
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Email Address")]        
        public string Value { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsPrimary { get; set; } = true;

    }

    public class Address : BusinessRuleList
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(50, MinimumLength = 10)]
        [Display(Name = "Address Line 1")]
        public string Line1 { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(50, MinimumLength = 10)]
        [Display(Name = "Address Line 2")]
        public string Line2 { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "City")]
        public string City { get; set; }


        [Required]
        [DataType(DataType.Text)]        
        [Display(Name = "State")]
        [StringLength(50, MinimumLength = 5)]
        public string State { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [StringLength(8, MinimumLength = 4)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Landmark")]
        public string Landmark { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime? ModifiedOn { get; set; }

        public int AddressTypeId { get; set; }

        public int PersonID { get; set; }

        public SelectList AddressTypes { get; set; }

        public string AddressType { get; set; }



    }

    public class Contact : BusinessRuleList
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Number")]
        //[RegularExpression(@"^[0-9]",ErrorMessage ="Invalid contact number")]
        public string Value { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime? ModifiedOn { get; set; }

        public string ContactNumberType { get; set; }

        public int ContactNumberTypeId { get; set; }

        public SelectList ContactNumberTypes { get; set; }

        public int PersonID { get; set; }

    }

    public class PersonType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CretaedOn { get; set; }
    }

    public class GenderType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Salutation
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class AddressType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ContactNumberType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }    

    public class Parent : BusinessRuleList
    {
        public int ChildPersonId { get; set; }
        public Person Father { get; set; }
        public Person Mother { get; set; }
    }

   

}
