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
    
    public partial class CPR_Incident_Status
    {
        public CPR_Incident_Status()
        {
            this.CPR_Incident = new HashSet<CPR_Incident>();
        }
    
        public int Incident_Status_Id { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Definition { get; set; }
    
        public virtual ICollection<CPR_Incident> CPR_Incident { get; set; }
    }
}
