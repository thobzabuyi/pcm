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
    
    public partial class ACM_IDPDevelopmentalAreaGenerosity
    {
        public int Generosity_Id { get; set; }
        public int IndividualDevelopmentPlan_Id { get; set; }
        public string ServiceToOthers { get; set; }
        public string PeacefulConflictResolution { get; set; }
        public string Caring { get; set; }
        public string Honesty { get; set; }
        public string Respect { get; set; }
        public string StrengthsAndResources { get; set; }
        public string NeedsConcerns { get; set; }
        public string ChangesWanted { get; set; }
        public string ActionsToEffectChange { get; set; }
    
        public virtual ACM_IndividualDevelopmentPlan ACM_IndividualDevelopmentPlan { get; set; }
    }
}
