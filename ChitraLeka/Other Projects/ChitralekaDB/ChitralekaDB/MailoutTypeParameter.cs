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
    
    public partial class MailoutTypeParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MailoutTypeParameter()
        {
            this.MailoutQueueParameterValue = new HashSet<MailoutQueueParameterValue>();
        }
    
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int MailoutTypeId { get; set; }
        public int MailoutParameterId { get; set; }
    
        public virtual MailoutType MailoutType { get; set; }
        public virtual MailoutParameter MailoutParameter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MailoutQueueParameterValue> MailoutQueueParameterValue { get; set; }
    }
}
