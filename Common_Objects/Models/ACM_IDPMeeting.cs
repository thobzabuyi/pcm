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
    
    public partial class ACM_IDPMeeting
    {
        public ACM_IDPMeeting()
        {
            this.ACM_IDPMeetingAttendees = new HashSet<ACM_IDPMeetingAttendees>();
        }
    
        public int Meeting_Id { get; set; }
        public int IndividualDevelopmentPlan_Id { get; set; }
        public Nullable<System.DateTime> MeetingDate { get; set; }
    
        public virtual ACM_IndividualDevelopmentPlan ACM_IndividualDevelopmentPlan { get; set; }
        public virtual ICollection<ACM_IDPMeetingAttendees> ACM_IDPMeetingAttendees { get; set; }
    }
}
