//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChitraLeka.Other_Projects.ChitralekaDB.ChitralekaDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class CalendarDetail
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public System.DateTime DueOn { get; set; }
        public bool HasLapsed { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Comments { get; set; }
    
        public virtual Calendar Calendar { get; set; }
    }
}
