using ChitraLeka.ViewModel.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ChitraLeka.ViewModel.Shared
{
    public class Message
    {
        public DateTime CreateOn { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public bool Show { get; set; }
        public bool HasRead { get; set; }
    }

    public class Support
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Message cannot be blank")]
        [DataType(DataType.Text)]
        [MaxLength(250, ErrorMessage = "Message length cannot be more than 250 characters")]
        public string Message { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter a valid emailaddress")]
        public string EmailAddress { get; set; }

        public bool isMemnber { get; set; } = true;
    }

    public class Term
    {

        public int Id { get; set; }

        [Display(Name = "Term Name")]
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Term Start Date")]
        [Required]
        //[DataType(DataType.Date)]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "Term End Date")]
        [Required]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(80);

        [Display(Name = "Number of weeks")]
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid number of weeks")]
        [Range(10, 15, ErrorMessage = "Invalid number of weeks")]
        public short WeeksCount { get; set; }

        [Display(Name = "Number of days")]
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid number of days")]
        [Range(50, 100, ErrorMessage = "Invalid number of days")]
        public short DaysCount { get; set; }

        [Required]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid unit price; Maximum two decimal points.")]
        [Range(5, 14.99, ErrorMessage = "Invalid unit price; Max 18 digits")]
        public decimal UnitPrice { get; set; }

        [Required]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid term fee; Maximum two decimal points.")]
        [Range(5, 150.99, ErrorMessage = "Invalid term fee; Max 18 digits")]
        public decimal Fee { get; set; }

        public string Remarks { get; set; }
        public Shared.Grade Grade { get; set; }

        public int GradeId { get; set; }
        public int LocationId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }

    }

    public class TermCreation : Term
    {
        public SelectList Grades { get; set; }

        public SelectList Locations { get; set; }
    }

    public class TermListing
    {
        public List<Term> TermList { get; set; }
    }

    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudentCount { get; set; }
    }

    public class AcademicCenter
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MxType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlBody { get; set; }
    }

    public class MailoutCompose
    {

        public int PersonTypeId { get; set; }
        public SelectList PersonTypeList { get; set; }

        [Display(Name = "Sub Category")]
        [Required]
        public int Grade { get; set; }
        public SelectList GradeList { get; set; }

        [Display(Name = "Target")]
        [Required]
        [DataType(DataType.Text)]
        public string Target { get; set; }

        [Display(Name = "PersonListId")]
        [Required]
        [DataType(DataType.Text)]
        public string PersonListId { get; set; }


        [Display(Name = "Subject")]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "From Email Address")]
        public string From { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5)]
        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }

    public class MailoutQueue
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public string Email { get; set; }
        public Person Person { get; set; }
        public string HTML { get; set; }
        public int MailoutTypeId { get; set; }
        public int Status { get; set; }
        //public int Subject { get; set; }
        public MxType Type { get; set; }
    }
}