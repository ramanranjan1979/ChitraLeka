using ChitraLeka.ViewModel.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ChitraLeka.ViewModel.Registration
{

    public class RegistrationList
    {
        public List<Registration> Registrations { get; set; }
    }

    public class Registration : BusinessRuleList
    {
        public int RegistrationId { get; set; }

        public int StudentId { get; set; }

        public Person Candidate { get; set; }

        public bool IsAdult { get; set; } = false;

        public Person CandidateFather { get; set; }

        public Person CandidateMother { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }       

        #region dropdowns
        public int Grade { get; set; }
        [Display(Name = "Grade")]
        public string GradeName { get; set; }
        public SelectList GradeTypes { get; set; }
        #endregion

        [Display(Name = "Registration Date")]
        public DateTime CreatedOn { get; set; }
    }     


}