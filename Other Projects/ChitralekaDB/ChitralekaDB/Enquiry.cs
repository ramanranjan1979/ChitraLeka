//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChitralekaDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Enquiry
    {
        public int Id { get; set; }
        public int EnquiryTypeId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool IsOpen { get; set; }
        public int PersonId { get; set; }
    
        public virtual EnquiryType EnquiryType { get; set; }
        public virtual Person Person { get; set; }
    }
}