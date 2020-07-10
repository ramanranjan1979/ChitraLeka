using ChitraLeka.ViewModel.Contact;
using ChitraLeka.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ChitraLeka.ViewModel.Student
{

    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        // [DataType(DataType.Date)]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [Display(Name = "Admission Date")]
        public DateTime DateOfAdmission { get; set; } = DateTime.Now;

        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        public string Grade { get; set; }

        public string Term { get; set; }

        public int TermId { get; set; }

        public int RegistrationId { get; set; }

        public int AcademyCenterId { get; set; }
        public SelectList AcademyCenters { get; set; }

        [Required]
        // [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        // [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(80);

        public bool IsCurrent { get; set; } = true;

        public StudentGradeList StudentGrades { get; set; }

        public SelectList GradeTerms { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public Person Candidate { get; set; }


    }

    public class StudentGradeList
    {
        public List<Student> StudentGrades { get; set; }
    }



}