using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ChitraLeka.ViewModel.Ledger
{
    public class InvoiceType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class InvoiceListing
    {
        public List<Invoice> InvoiceList { get; set; }
    }

    public class Invoice
    {
        public int Id { get; set; }

        public int InvoiceTypeId { get; set; }

        public string InvoiceNumber { get; set; }

        public string InvoiceDescrition { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        [Required]
        //[DataType(DataType.Date)]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(5);

        public bool IsDue { get; set; }

        [Required]        
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Invoice Amount; Maximum Two Decimal Points.")]
        [Range(1, 9999999999999999.99, ErrorMessage = "Invalid Invoice Amount; Max 18 digits")]
        public decimal InvoiceAmount { get; set; }

        [DataType(DataType.MultilineText)]        
        public string Note { get; set; }

        public int PersonId { get; set; }

        public int StudentId { get; set; }

        public ChitraLeka.ViewModel.Student.Student Student { get; set; }

        public ChitraLeka.ViewModel.Contact.Person Person { get; set; }
        
    }

    public class InvoiceCreation : Invoice
    {
        public SelectList InvoiceTypes { get; set; }
        
        public SelectList StudentList { get; set; }
    }
}

