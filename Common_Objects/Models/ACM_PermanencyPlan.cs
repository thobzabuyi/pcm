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
    
    public partial class ACM_PermanencyPlan
    {
        public ACM_PermanencyPlan()
        {
            this.ACM_PermanencyPlanPersons = new HashSet<ACM_PermanencyPlanPersons>();
        }
    
        public int PermanencyPlan_Id { get; set; }
        public int ChildrensCourtReport_Id { get; set; }
        public bool IsPlacedInFosterCare { get; set; }
        public bool IsAdoptedByRelatives { get; set; }
        public bool IsGuardianship { get; set; }
        public bool IsNonRelatives { get; set; }
        public bool IsPermanentClusterCare { get; set; }
        public string Circumstances { get; set; }
        public string Suitablitiy { get; set; }
    
        public virtual ACM_ChildrensCourtReport ACM_ChildrensCourtReport { get; set; }
        public virtual ICollection<ACM_PermanencyPlanPersons> ACM_PermanencyPlanPersons { get; set; }
    }
}