//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common_Objects.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ACM_YesNoOption
    {
        public ACM_YesNoOption()
        {
            this.ACM_IDPMeetingPersonPresence = new HashSet<ACM_IDPMeetingPersonPresence>();
        }
    
        public int YesNoOption_Id { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<ACM_IDPMeetingPersonPresence> ACM_IDPMeetingPersonPresence { get; set; }
    }
}
